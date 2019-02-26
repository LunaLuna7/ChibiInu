using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public bool MovingLaser;
    public float moveSpeed = 0;
    public List<Transform> patrolLocations;
    private int nextPatrolLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (MovingLaser)
            Patrol();
	}

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!collision.GetComponent<CharacterController2D>().m_Immune)
                collision.GetComponent<PlayerHealth>().gameManager.GameOver(collision.gameObject.transform);
        }
    }

    private void Patrol()
    {
        
        transform.position = Vector2.MoveTowards(transform.position,
            patrolLocations[nextPatrolLocation].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolLocations[nextPatrolLocation].position) <= 2)
        {
            nextPatrolLocation = (nextPatrolLocation + 1) % patrolLocations.Count;
        }
    }
}
