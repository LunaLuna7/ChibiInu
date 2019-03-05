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
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public GameObject player;
    [System.NonSerialized] public PlayerHealth playerHealth;

    public bool playerInRange;
    private Collider2D col;
    public Collider2D IgnoreCollision = null;

    public bool killed = false; //If the player killed the enemy
    public bool permaDead = false; //if the player past the checkPoint

 
    void Start () {
        playerInRange = false;
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        if(IgnoreCollision != null)
            Physics2D.IgnoreCollision(IgnoreCollision.GetComponent<Collider2D>(), col);

    }
	
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
        stateTimeElapsed = 0;
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

    public bool CheckIfCountDOwnElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }
   

}
