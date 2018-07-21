using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float speed;
    public float lifeSpam = 3f;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeSpam);
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
