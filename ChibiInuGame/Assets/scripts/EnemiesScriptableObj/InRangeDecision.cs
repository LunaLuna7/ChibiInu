using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/InRange")]
public class InRangeDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool playerInRange = InRange(controller);
        return playerInRange;
    }

    private bool InRange(StateController controller)
    {
        //RaycastHit2D hit;
        Debug.DrawRay(controller.transform.position, controller.transform.forward.normalized * controller.enemyStats.lookRange, Color.red);
        //if (Physics2D.CircleCast(controller.transform.position, controller.enemyStats.lookRange, new Vector2(0,0)))
        if(controller.playerInRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
