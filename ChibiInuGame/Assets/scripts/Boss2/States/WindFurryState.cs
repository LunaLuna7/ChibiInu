using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFurryState : IState {

	BossWorld2 controller;

    public WindFurryState(BossWorld2 c)
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
        Debug.Log("Executing wind furry State");
    }

    public void ExitState()
    {
        controller.inState = false;
    }
}
