using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/BossChase")]
public class BossChaseAction : Action {

    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    void Chase(StateController controller)
    {
        if(controller.player.transform.position.x - controller.transform.position.x > 33)
            controller.transform.position = Vector2.MoveTowards(controller.transform.position, controller.player.transform.position, controller.enemyStats.moveSpeed * Time.deltaTime * 2);
        else
            controller.transform.position = Vector2.MoveTowards(controller.transform.position, controller.player.transform.position, controller.enemyStats.moveSpeed * Time.deltaTime);

    }
}
