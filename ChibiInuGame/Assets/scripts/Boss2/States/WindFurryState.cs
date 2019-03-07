using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFurryState : IState {

	BossWorld2 controller;
    private float speed = 15f;
    private float windLifeTime = 2f;
    private int windMagnitude = 300;
    private Color color = Color.green;

    public WindFurryState(BossWorld2 c)
    {
        controller = c;
    }

    public void EnterState()
    {
        controller.inState = true;
        controller.movementController.StopMoving();
        controller.StartCoroutine(WindSkill());
    }

    public void ExecuteState()
    {
        //leave the state after 4 seconds
        if (controller.CheckIfCountDownElapsed(4f))
        {
            controller.stateTimeElapsed = 0;
            this.ExitState();
        }
    }

    public void ExitState()
    {
        controller.inState = false;
        controller.movementController.ContinueMoving();
    }

    public IEnumerator WindSkill()
    {
        yield return controller.cloudController.ChangeColorTo(color, 1f);
        yield return new WaitForSeconds(1);
        //instantiate wind and move to four direction
        GameObject upWind = GameObject.Instantiate(controller.wind, controller.transform.position, Quaternion.identity);
        upWind.transform.SetParent(controller.skillObjectsGroup);
        upWind.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        //rotate the up&&down wind
        //upWind.transform.Rotate(0, 0, 90);
        //change force direction and magnitude, if rotated object need to rotate force angle as well
        upWind.GetComponent<AreaEffector2D>().forceAngle = 90;
        upWind.GetComponent<AreaEffector2D>().forceMagnitude = windMagnitude;
        //be cleaned after a period
        MonoBehaviour.Destroy(upWind, windLifeTime);

        GameObject downWind = GameObject.Instantiate(controller.wind, controller.transform.position, Quaternion.identity);
        downWind.transform.SetParent(controller.skillObjectsGroup);
        downWind.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        downWind.transform.Rotate(0, 0, 180);
        downWind.GetComponent<AreaEffector2D>().forceAngle = 90;
        downWind.GetComponent<AreaEffector2D>().forceMagnitude = windMagnitude;
        MonoBehaviour.Destroy(downWind, windLifeTime);

        GameObject leftWind = GameObject.Instantiate(controller.wind, controller.transform.position, Quaternion.identity);
        leftWind.transform.SetParent(controller.skillObjectsGroup);
        leftWind.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        leftWind.transform.Rotate(0, 0, 90);
        leftWind.GetComponent<AreaEffector2D>().forceAngle = 90;
        leftWind.GetComponent<AreaEffector2D>().forceMagnitude = windMagnitude;
        MonoBehaviour.Destroy(leftWind, windLifeTime);

        GameObject rightWind = GameObject.Instantiate(controller.wind, controller.transform.position, Quaternion.identity);
        rightWind.transform.SetParent(controller.skillObjectsGroup);
        rightWind.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        rightWind.transform.Rotate(0, 0, 270);
        rightWind.GetComponent<AreaEffector2D>().forceAngle = 90;
        rightWind.GetComponent<AreaEffector2D>().forceMagnitude = windMagnitude;
        MonoBehaviour.Destroy(rightWind, windLifeTime);
        //ignore each other
        Physics2D.IgnoreCollision(upWind.GetComponent<Collider2D>(), downWind.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(upWind.GetComponent<Collider2D>(), leftWind.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(upWind.GetComponent<Collider2D>(), rightWind.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(downWind.GetComponent<Collider2D>(), leftWind.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(downWind.GetComponent<Collider2D>(), rightWind.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(leftWind.GetComponent<Collider2D>(), rightWind.GetComponent<Collider2D>());
    }
}
