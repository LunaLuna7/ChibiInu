using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastSpikeState : IState{

	BossWorld2 controller;

    public FastSpikeState(BossWorld2 c)
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
