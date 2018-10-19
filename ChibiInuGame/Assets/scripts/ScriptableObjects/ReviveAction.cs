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
        controller.gameObject.SetActive(true);
    }
}
