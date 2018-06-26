using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectile : MonoBehaviour {

    public bool UP;
    public bool DOWN;
    public bool RIGHT;
    public bool LEFT;

    public int speed;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        DestroyObject(gameObject, 3);
        rb = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (DOWN)
        {
            rb.velocity = -transform.up * speed;
        }
        if (UP)
        {
            rb.velocity = transform.up * speed;
        }
        if (RIGHT)
        {
            rb.velocity = transform.right * speed;
        }
        if (LEFT)
        {
            rb.velocity = -transform.right * speed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            DestroyObject(gameObject);
    }
}
