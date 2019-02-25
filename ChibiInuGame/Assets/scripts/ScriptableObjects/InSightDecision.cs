using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/InSight")]
public class InSightDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool playerVisible;
        playerVisible = !controller.player.GetComponent<CharacterController2D>().m_OnShield;

        return playerVisible;
    }

}
