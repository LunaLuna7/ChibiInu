using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeState : IState{

    BossWorld2 controller;
    private Color color = Color.blue;
    
    public SpikeState(BossWorld2 c)
    {
        controller = c;
    }

    public void EnterState(){
        controller.inState = true;
        controller.spikesLeft.SetActive(true);
        controller.spikesRight.SetActive(true);
        Debug.Log("Entering Spikes");
	}
	
	public void ExecuteState(){
        if (controller.CheckIfCountDownElapsed(5f))
        {
            controller.stateTimeElapsed = 0;
            this.ExitState();
        }
        Debug.Log("Executing Spikes");
	}
	
	public void ExitState(){
        controller.inState = false;
        Debug.Log("Leaving Spikes");
    }
    
   
}
