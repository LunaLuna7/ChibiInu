﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPlayer : MonoBehaviour {

    [Header("Player stats")]
    public int health;
    public int speed;
    public float jumpPower;
    public float fallGravity;
    public float lowJumpGravity;

    private int originalspd;

    [Space]

    [Header("Player state")]
    public float moveX;
    public int jumpsLeft;
    public int maxJumps;
    public bool facingRight;
    public bool Isgrounded;
    public SpriteRenderer sprite;
    Rigidbody2D rb;
	
	void Awake () {
        originalspd = speed;
        maxJumps = jumpsLeft;
        Isgrounded = true;
        facingRight = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        move();
        Jump();
        JumpGravity();
        Dash();
	}
    void move()
    {
        moveX = Input.GetAxis("Horizontal");
        if(moveX < 0.0f && facingRight == true)//moving right
        {

            FlipPlayer();
        }
        else if(moveX > 0.0f && facingRight == false)//moving left
        {
            FlipPlayer();
        }
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Isgrounded = true;
            jumpsLeft = maxJumps;
        }
    }

    void Jump()
    {
       
        if (Input.GetButtonDown("Jump") && Isgrounded == true)
        {
            if(jumpsLeft == 1)
            {
                Isgrounded = false;
            }
            jumpsLeft--;
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


    void FlipPlayer()
    {
        facingRight = !facingRight;
                
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
        
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            
            speed *= 2;
            StartCoroutine(regularSpeed());
            
        }
    }

    IEnumerator regularSpeed()
    {
        yield return new WaitForSeconds(.4f);
        speed = originalspd;
    }
}

