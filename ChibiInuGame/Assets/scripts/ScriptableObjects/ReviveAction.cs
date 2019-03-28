using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Revive")]

public class ReviveAction : Action {

    public override void Act(StateController controller)
    {
        Revive(controller);
    }

    void Revive(StateController controller)
    {
        controller.m_SpriteRender.enabled = false;
        controller.gameObject.transform.localPosition = Vector3.zero;
        controller.m_SpriteRender.enabled = true;
        controller.health = controller.enemyStats.HP;
    }
}
