using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFurryState : IState {

	BossWorld2 controller;
    private float speed = 5f;
    private float windLifeTime = 20f;
    private int windMagnitude = 120;
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
        upWind.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        //rotate the up&&down wind
        upWind.transform.Rotate(0, 0, 90);
        //change force direction and magnitude, if rotated object need to rotate force angle as well
        upWind.GetComponent<AreaEffector2D>().forceAngle = 90 - 90;
        upWind.GetComponent<AreaEffector2D>().forceMagnitude = windMagnitude;
        //be cleaned after a period
        MonoBehaviour.Destroy(upWind, windLifeTime);

        GameObject downWind = GameObject.Instantiate(controller.wind, controller.transform.position, Quaternion.identity);
        downWind.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        downWind.transform.Rotate(0, 0, 90);
        downWind.GetComponent<AreaEffector2D>().forceAngle = 270 - 90;
        downWind.GetComponent<AreaEffector2D>().forceMagnitude = windMagnitude;
        MonoBehaviour.Destroy(downWind, windLifeTime);

        GameObject leftWind = GameObject.Instantiate(controller.wind, controller.transform.position, Quaternion.identity);
        leftWind.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
        leftWind.GetComponent<AreaEffector2D>().forceAngle = 180;
        leftWind.GetComponent<AreaEffector2D>().forceMagnitude = windMagnitude;
        MonoBehaviour.Destroy(leftWind, windLifeTime);

        GameObject rightWind = GameObject.Instantiate(controller.wind, controller.transform.position, Quaternion.identity);
        rightWind.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        rightWind.GetComponent<AreaEffector2D>().forceAngle = 0;
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
