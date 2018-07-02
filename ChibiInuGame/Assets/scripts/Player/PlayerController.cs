using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Player stats")]

    public float speed;
    public float jumpPower;
    public float fallGravity;
    public float lowJumpGravity;
    
    [Header("Player States")]
    public bool facingRight = false;
    public float moveX;
    public int jumpsLeft;
    public int maxJumps;
    public bool grounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool onWall;
    public Transform wallCheck;
    public LayerMask wallLayer;
    public bool wallSliding;

    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        if (grounded)
            jumpsLeft = maxJumps;

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            rb.velocity = Vector2.up * jumpPower;
            jumpsLeft--;
        }
        else if (Input.GetButtonDown("Jump") && jumpsLeft == 0 && grounded == true)
        {
            rb.velocity = Vector2.up * jumpPower;
        }
        JumpGravity();

        if (!grounded)
        {
            onWall = Physics2D.OverlapCircle(wallCheck.position,.2f, wallLayer);
            
            
            //if(facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") <= 0.1f)
            //{
                if (onWall)
                {
                    WallSliding();
                }
           // }
        }
        if(!wallCheck || grounded)
        {
            wallSliding = false;
        }
    }

    void WallSliding()
    {
        rb.velocity = new Vector2(rb.velocity.x, -.5f);
        wallSliding = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                Flip();
                rb.AddForce(new Vector2(-3, 50) * jumpPower);
            }
            else
            {
                Flip();
                rb.AddForce(new Vector2(3, 50) * jumpPower);
            }
        }
    }

    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (moveX < 0 && facingRight == true)//moving right
            Flip();
        
        else if (moveX > 0 && facingRight == false)//moving left
            Flip();


        
    }

    void JumpGravity()
    {
        if (rb.velocity.y < 0) //we are falling
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))//tab jump
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpGravity - 1) * Time.deltaTime;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;

        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    
}
