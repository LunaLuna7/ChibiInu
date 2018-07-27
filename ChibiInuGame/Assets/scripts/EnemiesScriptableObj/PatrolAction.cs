using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action {

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        //Look(controller, controller.patrolLocations[controller.nextPatrolLocation]);
        controller.transform.position = Vector2.MoveTowards(controller.transform.position, 
            controller.patrolLocations[controller.nextPatrolLocation].position, controller.enemyStats.moveSpeed * Time.deltaTime);

        if (Vector2.Distance(controller.transform.position, controller.patrolLocations[controller.nextPatrolLocation].position) <= 2)
        {
            Flip(controller);
            controller.nextPatrolLocation = (controller.nextPatrolLocation + 1) % controller.patrolLocations.Count;
        }
    }

    private void Look(StateController controller, Transform toLook)
    {
        Vector3 dir = toLook.position - controller.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        controller.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Flip(StateController controller)
    {
        Vector2 localScale = controller.gameObject.transform.localScale;
        localScale.x *= -1;
        controller.transform.localScale = localScale;
    }
}
