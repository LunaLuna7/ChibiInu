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
        controller.transform.position = Vector2.MoveTowards(controller.transform.position, 
            controller.patrolLocations[controller.nextPatrolLocation].position, controller.enemyStats.moveSpeed * Time.deltaTime);

        Flip(controller);
        if (Vector2.Distance(controller.transform.position, controller.patrolLocations[controller.nextPatrolLocation].position) <= 2)
        {
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
        if (controller.transform.position.x - controller.patrolLocations[controller.nextPatrolLocation].position.x > 0)
            localScale.x = -1;
        else
            localScale.x = 1;
        controller.transform.localScale = localScale;
    }
}
