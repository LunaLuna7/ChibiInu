﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteSpikeSongState : IState {

	BossWorld2 controller;
	private float minY = -2;
    private float maxY = 23;
    private float minX = -173.4f;
    private float maxX = -116.9f;
    private float speed = 20;

    public FluteSpikeSongState(BossWorld2 c)
    {
        controller = c;
    }

    public void EnterState()
    {
        controller.inState = true;
        FluteSongSkill();
    }

    public void ExecuteState()
    {
        //leave the state after 5 seconds
        if (controller.CheckIfCountDownElapsed(5f))
        {
            controller.stateTimeElapsed = 0;
            this.ExitState();
        }
        Debug.Log("Executing Flute Spikes Song State");
    }

    public void ExitState()
    {
        controller.inState = false;
    }

    public void FluteSongSkill()
    {
        controller.StartCoroutine(ThrowOneFluteUp(true, speed));
        controller.StartCoroutine(ThrowOneFluteDown(false, speed));
    }

    public IEnumerator ThrowOneFluteDown(bool right, float speed)
    {
        //by default towards left
        Vector2 position = new Vector2(maxX, Random.Range((minY + maxY)/2, maxY));
        //generate position
        if(right)
            position.x = minX;
        //generate the flute and....move it
        GameObject fluteSpike = GameObject.Instantiate(controller.fluteSpike, position, Quaternion.identity);
        yield return MoveFlute(fluteSpike, right, false);
        //remove the flute spike after finishing
        GameObject.Destroy(fluteSpike);
    }

    public IEnumerator ThrowOneFluteUp(bool right, float speed)
    {
        //by default towards left
        Vector2 position = new Vector2(maxX, Random.Range(minY, (minY + maxY)/2));
        //generate position
        if(right)
            position.x = minX;
        //generate the flute and....move it
        GameObject fluteSpike = GameObject.Instantiate(controller.fluteSpike, position, Quaternion.identity);
        yield return MoveFlute(fluteSpike, right, true);
        //remove the flute spike after finishing
        GameObject.Destroy(fluteSpike);
    }

    private IEnumerator MoveFlute(GameObject fluteSpike, bool right, bool up)
    {
        float xRange = maxX - minX;
        float yScale = (maxY - minY)/2;
        float initialY = fluteSpike.transform.position.y;
        if(right)
        {
            for(float x = fluteSpike.transform.position.x; x <= maxX; x += 0.2f)
            {
                //scale x to 0 - 2 pi
                float xOffSet = (fluteSpike.transform.position.x - minX)* 6.28f/xRange;
                //(Mathf.Cos(xOffSet) - 1)/2 gives a float from 0-1
                float yMove = (Mathf.Cos(xOffSet) - 1)/2f * yScale;
                //if go up, need to reverse yMove to make it positive
                if(up)
                    yMove = - yMove;
                fluteSpike.GetComponent<Rigidbody2D>().MovePosition(new Vector2(x, initialY + yMove));
                yield return new WaitForSeconds(0.1f/speed);
            }
        }else{
            for(float x = fluteSpike.transform.position.x; x >= minX; x -= 0.2f)
            {
                //scale x to 0 - 2 pi
                float xOffSet = (fluteSpike.transform.position.x - minX)* 6.28f/xRange;
                //(Mathf.Cos(xOffSet) - 1)/2 gives a float from 0-1
                float yMove = (Mathf.Cos(xOffSet) - 1)/2f * yScale;
                //if go up, need to reverse yMove to make it positive
                if(up)
                    yMove = - yMove;
                fluteSpike.GetComponent<Rigidbody2D>().MovePosition(new Vector2(x, initialY + yMove));
                yield return new WaitForSeconds(0.1f/speed);
            }
        }
    }

}
