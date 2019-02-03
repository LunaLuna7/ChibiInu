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

    public FastSpikeState(BossWorld2 c)
    {
        controller = c;
    }

    public void EnterState()
    {
        controller.inState = true;
        FastSpikeSkill();
    }

    public void ExecuteState()
    {
        //leave the state after 7 seconds
        if (controller.CheckIfCountDownElapsed(7f))
        {
            controller.stateTimeElapsed = 0;
            this.ExitState();
        }
        Debug.Log("Executing Fast Spikes State");
    }

    public void ExitState()
    {
        controller.inState = false;
    }

    public void FastSpikeSkill()
    {
        for(int x = 0; x<= 4; ++x)
        {
            controller.StartCoroutine(ThrowOneFastSpike());
        }
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
        //if direction is vertical, change rotate 
        if(direction == Vector2.up || direction == Vector2.down)
            warningOb.transform.Rotate(0,0,90);
        yield return new WaitForSeconds(1f);
        
        //throw ob
        GameObject projectileOb = GameObject.Instantiate(controller.fastSpike, position, Quaternion.identity);
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
