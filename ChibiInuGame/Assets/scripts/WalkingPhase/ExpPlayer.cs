using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPlayer : MonoBehaviour {

    [Header("Player stats")]
    public int health;
    public int speed;
    public float jumpPower;
    public float fallGravity;
    public float lowJumpGravity;

    [Space]

    [Header("Player state")]
    public bool Isgrounded;

    Rigidbody2D rb;
	
	void Awake () {
        Isgrounded = true;
        rb = gameObject.GetComponent<Rigidbody2D>();		
	}
	
	void Update () {
        move();
        Jump();
        JumpGravity();

	}
    void move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        /*
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * jumpForce * Time.deltaTime;
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Isgrounded = true;
    }

    void Jump()
    {
       
        if (Input.GetButtonDown("Jump") && Isgrounded == true)
        {
            Isgrounded = false;
            rb.velocity = Vector2.up * jumpPower;
        }
    }
    void JumpGravity()
    {
        if(rb.velocity.y < 0) //we are falling
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.deltaTime;

        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))//tab jump
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpGravity - 1) * Time.deltaTime;
        }
    }
}
