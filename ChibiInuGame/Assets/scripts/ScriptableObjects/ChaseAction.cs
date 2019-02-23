using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action {

    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    void Chase(StateController controller)
    {
        Flip(controller);
        controller.rb.MovePosition(Vector3.Lerp(controller.transform.position, controller.player.transform.position, controller.enemyStats.moveSpeed));
        controller.transform.position = Vector2.MoveTowards(controller.transform.position, controller.player.transform.position, controller.enemyStats.moveSpeed * Time.deltaTime);
    }


    private void Flip(StateController controller)
    {
        Vector2 localScale = controller.gameObject.transform.localScale;
        if (controller.transform.position.x - controller.player.transform.position.x < 0)
            localScale.x = 1;
        else
            localScale.x = -1;
        controller.transform.localScale = localScale;
    }
}
