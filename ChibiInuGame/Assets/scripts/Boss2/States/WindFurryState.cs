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

    }

    public void ExitState()
    {
        controller.inState = false;
    }
}
