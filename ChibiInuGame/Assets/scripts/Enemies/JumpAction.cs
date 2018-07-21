using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Jump")]
public class JumpAction : Action {

    public override void Act(StateController controller)
    {
        Jump(controller);
    }

    
    private void Jump(StateController controller)
    {
        if(controller.CheckIfCountDOwnElapsed(controller.enemyStats.jumpRate))
        {
            Debug.Log("Jump");
            controller.rb.AddForce(new Vector2(0,controller.enemyStats.jumpPower * 100));
            controller.stateTimeElapsed = 0;
            //controller.transform.position = new Vector2(0, 4);
        }

    }
    
   
}
