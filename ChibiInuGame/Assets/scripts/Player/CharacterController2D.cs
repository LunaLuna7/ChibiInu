﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    [SerializeField] private float m_JumpForce = 800f;
    [SerializeField] private float m_WallJumpForce = 3000f;
    [SerializeField] public int m_AirJumps = 0;
    [SerializeField] private float m_FallGravity = 4f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private LayerMask m_WallLayer;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_GroundCheck2;
    [SerializeField] private Transform m_WallCheck;
    [SerializeField] private bool m_AirControl = false;


    [HideInInspector] public Rigidbody2D m_RigidBody2D;
    private bool m_Grounded;
    private bool m_OnWall;
    public bool m_limitLeftMove;
    public bool m_limitRightMove;
    public bool m_OnSwing;
    public bool m_FacingRight = true;
    private bool m_OnJumpPad = false;
    public bool m_Damaged;
    public bool m_Immune = false;
    public int m_AirJumpsLeft;
    private Vector3 m_Velocity = Vector3.zero;



    void Awake () {
        
        m_RigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer) || Physics2D.Linecast(transform.position, m_GroundCheck2.position, m_GroundLayer);
        m_OnWall = Physics2D.OverlapCircle(m_WallCheck.position, .2f, m_WallLayer);

        if (m_Grounded)
        {
            JumpadOff();
            m_AirJumpsLeft = m_AirJumps;
        }

    
    }

    public void Move(float move, bool jump)
    {
        if (m_limitRightMove && move > 0)
            move = 0;

        if (m_limitLeftMove && move < 0)
            move = 0;

        if(m_Grounded)
        {
            m_limitLeftMove = false;
            m_limitRightMove = false;
        }
     
        if (m_Grounded || m_AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, m_RigidBody2D.velocity.y);

            m_RigidBody2D.velocity = Vector3.SmoothDamp(m_RigidBody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }  

        }

        JumpGravity(jump);

        if (m_Grounded && jump && !m_OnWall && !m_OnSwing)
        {
            m_Grounded = false;
            m_RigidBody2D.AddForce(new Vector2(m_RigidBody2D.velocity.x, m_JumpForce));
        }

        //air Jump
        else if (jump && m_AirJumpsLeft > 0 && !m_OnWall && !m_OnSwing)
        {
            m_Grounded = false;
            m_RigidBody2D.AddForce(new Vector2(0f,  m_JumpForce));
            m_AirJumpsLeft--; //delay teh swing false
        }

        else if(jump && !m_Grounded && m_OnWall &&!m_OnSwing)
        {
            m_RigidBody2D.velocity = new Vector3();
            Flip();
            if (m_FacingRight)
            {
                m_limitLeftMove = true;
                m_RigidBody2D.AddForce(new Vector2(m_WallJumpForce, m_JumpForce));

            }

            else if (!m_FacingRight)
            {
                m_limitRightMove = true;
                m_RigidBody2D.AddForce(new Vector2(-m_WallJumpForce, m_JumpForce));
            }
            StartCoroutine(LimitWallJumpMove());

       
        }


    }
   

    void JumpGravity(bool jump)
    {

        if (jump && m_AirJumpsLeft >= 1)
        {

            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 0);
        }

        if (m_RigidBody2D.velocity.y < 0) //we are falling
        {
            m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
        }
        else if ((m_RigidBody2D.velocity.y > 0 || m_OnJumpPad) && !Input.GetButton("Jump"))//tab jump
        {
            m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
        }
        else if ((m_RigidBody2D.velocity.y > 0 || m_OnJumpPad )&& Input.GetButton("Jump") && m_OnJumpPad)
        {
            m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
        }
    }

    void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "hurtBox" && this.gameObject.transform.position.y - collide.gameObject.transform.position.y >= 0)
        {
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 25);
        }
       
    }
 
   
    //Used by other objects to check Character status
    public bool IsGrounded()
    {
        return m_Grounded;
    }

    public void JumpadOn()
    {
        m_OnJumpPad = true;
    }
    public void JumpadOff()
    {
        m_OnJumpPad = false;
    }

    IEnumerator LimitWallJumpMove()
    {

        yield return new WaitForSeconds(.6f);
        
        m_limitLeftMove = false;
        m_limitRightMove = false;
    }

}
