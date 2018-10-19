using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Die")]
public class DieAction : Action
{
	public override void Act(StateController controller)
    {
        Die(controller);
    }

    void Die(StateController controller)
    {
        controller.gameObject.SetActive(false);
    }
}
