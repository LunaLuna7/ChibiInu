using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    public State currentState;
    public EnemyStats enemyStats;
    public State remainState;
    public Transform originLocation;

    public List<Transform> patrolLocations;
    [System.NonSerialized] public int nextPatrolLocation;
    [System.NonSerialized] public Rigidbody2D rb;
    [System.NonSerialized] public float stateTimeElapsed;
    [System.NonSerialized] public GameObject player;
    [System.NonSerialized] public PlayerHealth playerHealth;
    public SpriteRenderer m_SpriteRender;

    public bool playerInRange;
    private Collider2D col;
    public Collider2D IgnoreCollision = null;

    [Header("Health Stats")]
    public bool killed = false; //If the player killed the enemy
    public bool permaDead = false; //if the player past the checkPoint
    public float health;
    public bool tempImmune;
 
    void Start () {
        killed = false;
        originLocation = transform;
        playerInRange = false;
        tempImmune = false;
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        m_SpriteRender = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        health = enemyStats.HP;
        if (IgnoreCollision != null)
            Physics2D.IgnoreCollision(IgnoreCollision.GetComponent<Collider2D>(), col);
    }
	
	void Update () {
        currentState.UpdateState(this);
	}

    private void OnDrawGizmos()
    {
        if(currentState != null)
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
        {
            if(Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) > 2 ||
                Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) > 5)
                playerInRange = false;

        }
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

    private void OnEnable()
    {
        //m_SpriteRender.enabled = true;
    }
}
