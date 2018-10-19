using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ToRevive")]

public class ReviveDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool revive = !PastCheckPoint(controller);
        return revive;
    }

    private bool PastCheckPoint(StateController controller)
    {
        return controller.permaDead;
    }
}
