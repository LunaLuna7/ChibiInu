using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState {

    BossWorld2 controller;

    public IdleState(BossWorld2 c)
    {
        controller = c;
    }

    public void EnterState()
    {
        controller.inState = true;
        Debug.Log("Entering Idle");
    }

    public void ExecuteState()
    {
        Debug.Log("Executin Idle");
        var timer = Random.Range(controller.minTime, controller.maxTime);
        if (controller.CheckIfCountDownElapsed(timer))
        {
            controller.stateTimeElapsed = 0;
            this.ExitState();
        }
    }

    public void ExitState()
    {
        controller.inState = false;
        Debug.Log("Leaving Idle");
    }
  
}
