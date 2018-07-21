using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    public State currentState;
    public EnemyStats enemyStats;
    public State remainState;

    public List<Transform> patrolLocations;
    [HideInInspector] public int nextPatrolLocation;
    public bool playerInRange;
    // Use this for initialization
    void Start () {
        playerInRange = false;

    }
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState(this);
	}

    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(gameObject.transform.position, enemyStats.lookRange);
        }
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public void OnExitState()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            playerInRange = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerInRange = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
