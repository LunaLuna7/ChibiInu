using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteSpikeSongState : IState {

	BossWorld2 controller;
	private float minY = -2;
    private float maxY = 23;
    private float minX = -173.4f;
    private float maxX = -116.9f;

    public FluteSpikeSongState(BossWorld2 c)
    {
        controller = c;
    }

    public void EnterState()
    {
        controller.inState = true;
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
}
