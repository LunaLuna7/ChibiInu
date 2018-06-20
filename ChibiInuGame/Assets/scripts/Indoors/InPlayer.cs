using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlayer : MonoBehaviour {

    public float moveX;
    public float moveY;
    public bool facingRight;
    public float speed;
    private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}
    void move()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        if (moveX < 0.0f && facingRight == true)//moving right
        {

            //FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == false)//moving left
        {
            //FlipPlayer();
        }
        rb.velocity = new Vector2(moveY * speed, rb.velocity.y);
        rb.velocity = new Vector2(moveX * speed, rb.velocity.x);
    }
}
