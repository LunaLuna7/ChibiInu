using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastSpikeState : IState{
	BossWorld2 controller;
    private float minY = -2;
    private float maxY = 23;
    private float minX = -173.4f;
    private float maxX = -116.9f;
    private float projectileSpeed = 40f;

    private Color color = Color.red;

    public FastSpikeState(BossWorld2 c)
    {
        controller = c;
    }

    public void EnterState()
    {
        controller.inState = true;
        controller.StartCoroutine(FastSpikeSkill());
    }

    public void ExecuteState()
    {
        //leave the state after 7 seconds
        if (controller.CheckIfCountDownElapsed(7f))
        {
            controller.stateTimeElapsed = 0;
            this.ExitState();
        }
    }

    public void ExitState()
    {
        controller.inState = false;
    }

    public IEnumerator FastSpikeSkill()
    {
        yield return controller.cloudController.ChangeColorTo(color, 1f);
        int num = GetSpikeNumber(2, 2, 6, 15);
        for(int x = 0; x< num; ++x)
        {
            controller.StartCoroutine(ThrowOneFastSpike());
        }
    }

    /*based on Boss's health, deacide how many spikes should be generated
    start from initial, each interval (in percent) of health dropped, increase num by one
    at last clamp it in (min, max)*/
    public int GetSpikeNumber(int initial, int min, int max, float increaseInterval)
    {
        float percentHealthLose = (1 - controller.bossHealth.health/controller.bossHealth.maxHealth) * 100;
        int increaseNum = (int)(percentHealthLose/increaseInterval);
        return Mathf.Clamp(initial + increaseNum, min, max);
    }


    //generate one fast spike on the screen
    public IEnumerator ThrowOneFastSpike()
    {
        int ran = Random.Range(0, 4);
        Vector2 position;
        Vector2 direction;
        //generate projectile position and direction 
        switch(ran)
        {
            case 0: //towards right projectile
                position = new Vector2(minX - 1, Random.Range(minY, maxY));
                direction = Vector2.right;
                break;
            case 1: //towards left projectile
                position = new Vector2(maxX + 1, Random.Range(minY, maxY));
                direction = Vector2.left;
                break;
            case 2:////towards up projectile
                position = new Vector2(Random.Range(minX, maxX), minY - 1);
                direction = Vector2.up;
                break;
            default://towards down projectile
                position = new Vector2(Random.Range(minX, maxX), maxY + 1);
                direction = Vector2.down;
                break;
        }

        //show warning
        GameObject warningOb = GameObject.Instantiate(controller.warningBlock, position, Quaternion.identity);
        warningOb.transform.SetParent(controller.skillObjectsGroup);
        //if direction is vertical, change rotate 
        if(direction == Vector2.up || direction == Vector2.down)
            warningOb.transform.Rotate(0,0,90);
        yield return new WaitForSeconds(1f);
        
        //throw ob
        GameObject projectileOb = GameObject.Instantiate(controller.fastSpike, position, Quaternion.identity);
        projectileOb.transform.SetParent(controller.skillObjectsGroup);
        //change the rotation to make it seems right
        if(direction == Vector2.up)
            projectileOb.transform.Rotate(0,0,270);
        else if(direction == Vector2.right)
            projectileOb.transform.Rotate(0,0,180);
        else if(direction == Vector2.down)
            projectileOb.transform.Rotate(0,0,90);
        projectileOb.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        //wait until finish
        yield return new WaitForSeconds(2f);
        //destroy!!!!
        GameObject.Destroy(warningOb);
        GameObject.Destroy(projectileOb);
    }
}
