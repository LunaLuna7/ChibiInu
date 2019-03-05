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
        if (controller.playerHealth.HPLeft > 0 && Mathf.Abs(controller.player.transform.position.x - controller.transform.position.x) < 20 && Mathf.Abs(controller.player.transform.position.y - controller.transform.position.y) < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
