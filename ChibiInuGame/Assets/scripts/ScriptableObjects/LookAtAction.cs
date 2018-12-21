using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/LookAt")]
public class LookAtAction : Action {

    public override void Act(StateController controller)
    {
        if(controller.playerInRange)
            Flip(controller);
    }
    private void Flip(StateController controller)
    {
        Vector2 localScale = controller.gameObject.transform.localScale;
        if (controller.transform.position.x - controller.player.transform.position.x > 0)
            localScale.x = -1;
        else
            localScale.x = 1;
        controller.transform.localScale = localScale;
    }
}
