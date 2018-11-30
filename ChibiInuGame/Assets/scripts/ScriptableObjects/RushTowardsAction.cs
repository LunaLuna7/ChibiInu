using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/RushTowards")]
public class RushTowardsAction : Action {


    public override void Act(StateController controller)
    {
        RushTowards(controller);
    }

    private void RushTowards(StateController controller)
    {
        //Look(controller, controller.patrolLocations[controller.nextPatrolLocation]);

        if (controller.transform.position.x > controller.player.transform.position.x) //Player is on the left
        {
            controller.transform.position = Vector2.MoveTowards(controller.transform.position,
            controller.patrolLocations[0].position, controller.enemyStats.moveSpeed * Time.deltaTime);

        }
        else if( controller.transform.position.x <= controller.player.transform.position.x)//Player is on the right
        {
            controller.transform.position = Vector2.MoveTowards(controller.transform.position,
           controller.patrolLocations[0].position, controller.enemyStats.moveSpeed * Time.deltaTime);
        }

        
    }


}
