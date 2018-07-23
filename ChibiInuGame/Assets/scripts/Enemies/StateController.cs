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

    public GameObject attack;
    public GameObject arrow;
    public Transform attackSpawnPosition;

    public bool playerInRange;
    private Collider2D col;
    public Collider2D colOther = null;
    
    // Use this for initialization
    void Start () {
        playerInRange = false;
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(colOther != null)
            Physics2D.IgnoreCollision(colOther.GetComponent<Collider2D>(), col);

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
    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Physics2D.IgnoreCollision(col, collision.collider);
        }
    }*/

}
