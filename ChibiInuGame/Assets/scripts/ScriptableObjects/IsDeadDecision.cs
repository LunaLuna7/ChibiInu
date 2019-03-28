using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/IsDead")]

public class IsDeadDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool Dead = IsDead(controller);
        return Dead;
    }

    private bool IsDead(StateController controller)
    {
        return controller.killed || controller.playerHealth.HPLeft == 0;
    }
}
