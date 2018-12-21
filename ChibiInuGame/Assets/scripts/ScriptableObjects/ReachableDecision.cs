using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Reachable")]
public class ReachableDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool playerInRange = InRange(controller);
        return playerInRange;
    }

    private bool InRange(StateController controller)
    {
        if (Mathf.Abs(controller.player.transform.position.x - controller.transform.position.x) > 30)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
