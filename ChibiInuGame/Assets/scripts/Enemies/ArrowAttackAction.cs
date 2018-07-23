using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ArrowAttack")]
public class ArrowAttackAction : Action {

    public override void Act(StateController controller)
    {
        Arrow(controller);
    }

    private void Arrow(StateController controller)
    {
        if(!controller.arrow.activeSelf && controller.CheckIfCountDOwnElapsed(1f))
            controller.arrow.SetActive(true);

        if (controller.CheckIfCountDOwnElapsed(controller.enemyStats.attackRate))
        {
            Instantiate(controller.attack, controller.attackSpawnPosition.transform.position, controller.player.transform.rotation);
            controller.stateTimeElapsed = 0;
            controller.arrow.SetActive(false);
        }
    }

}
