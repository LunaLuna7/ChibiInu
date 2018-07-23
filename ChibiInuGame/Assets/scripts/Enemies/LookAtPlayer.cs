using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public GameObject player;
    private bool PlayerAtRight = true;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	

	void Update () {
		if((player.transform.position.x - transform.position.x) > 0 && PlayerAtRight) //player is at left side
        {
            PlayerAtRight = false;
            Flip();
        }
        else if(((player.transform.position.x - transform.position.x) <= 0) && !PlayerAtRight) //player is at right side
        {
            Flip();
            PlayerAtRight = true;
        }
    }
    private void Flip()
    {
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
