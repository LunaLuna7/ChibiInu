using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState {

    BossWorld2 controller;
    private Color color = Color.white;

    public IdleState(BossWorld2 c)
    {
        controller = c;
    }

    public void EnterState()
    {
        controller.inState = true;
        controller.movementController.StopMoving();
        controller.StartCoroutine(controller.cloudController.ChangeColorTo(color, 1f));
    }

    public void ExecuteState()
    {
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
    }
  
}
