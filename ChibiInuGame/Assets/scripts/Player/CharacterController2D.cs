﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

public class CharacterController2D : MonoBehaviour {

    [SerializeField] private float m_JumpForce = 900f;
    [SerializeField] private float m_WallJumpTime = .5f;
    [SerializeField] public int m_AirJumps = 0;
    [SerializeField] private float m_FallGravity = 4f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private LayerMask m_WallLayer;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_GroundCheck2;
    [SerializeField] private Transform m_WallCheck;
    [SerializeField] private bool m_AirControl = false;
    public Transform fireSpawn;

    [SerializeField] private float m_DashForce = 1000f;
    public int m_DashLeft;

    [HideInInspector] public Rigidbody2D m_RigidBody2D;
    private bool m_Grounded;
    private bool m_OnWall;
    private bool doingWallJump = false;
    private bool wallDeadTime;
    public bool m_limitLeftMove;
    public bool m_limitRightMove;
    public bool m_FacingRight = true;
    private bool m_OnJumpPad = false;
    public bool m_Damaged;
    public bool m_Immune = false;
    public int m_AirJumpsLeft;
    private Vector3 m_Velocity = Vector3.zero;

    public GameObject cam;

    //CoolDowns
    public bool m_GroundDash;
    public bool m_OnSwing;


    private IEnumerator activeJumpCoroutine;
    public float JumpProgress { get; private set; }

    void Awake () {
        m_GroundDash = true;
        m_DashLeft = 1;
        m_RigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer) || Physics2D.Linecast(transform.position, m_GroundCheck2.position, m_GroundLayer);
        m_OnWall = Physics2D.OverlapCircle(m_WallCheck.position, .2f, m_WallLayer);

        if (m_Grounded)
        {
            JumpadOff();
            m_AirJumpsLeft = m_AirJumps;
            /*if (!wallBlock.activeSelf)
                wallBlock.SetActive(true);*/
        }

        if (!m_Grounded)
        {
            /*if (wallBlock.activeSelf)
                wallBlock.SetActive(false);*/

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

        if (m_OnWall)
        {
            if((move < 0  && m_FacingRight) && wallDeadTime || (move > 0 && !m_FacingRight) && wallDeadTime)
            {
                move = 0;
                StartCoroutine(WallDeadTime());
            }
           
        }
        else if (!m_OnWall)
        {
            wallDeadTime = true;
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

        if (m_Grounded && jump && (!m_OnWall || m_OnWall) && !m_OnSwing)
        {
            m_Grounded = false;
            m_RigidBody2D.AddForce(new Vector2(m_RigidBody2D.velocity.x, m_JumpForce));
            m_DashLeft = 1;
        }

        //air Jump
        else if (jump && m_AirJumpsLeft > 0 && !m_OnWall && !m_OnSwing && !doingWallJump)
        {
            m_Grounded = false;
            m_RigidBody2D.AddForce(new Vector2(0f,  m_JumpForce));
            m_AirJumpsLeft--;
            m_DashLeft = 1;
        }


        //wall jump
        else if(jump && !m_Grounded && m_OnWall &&!m_OnSwing)
        {
            m_DashLeft = 1;
            
            Flip();
            if (m_FacingRight)
            {
                //m_limitLeftMove = true;
                //m_limitRightMove = false;


                //m_RigidBody2D.AddForce(new Vector2(m_WallJumpForce, m_JumpForce));
                //m_RigidBody2D.velocity = new Vector3(60, 20);
                ParabolaJump(transform.position + new Vector3(5, 5, 0), 3, m_WallJumpTime); //(5, 3f, 0), 3, m_wallJumpTime
                
                //StartCoroutine(LimitWallJumpMoveLeft());

            }

            else if (!m_FacingRight)
            {
                //m_limitRightMove = true;
                //m_limitLeftMove = false;
                //m_RigidBody2D.AddForce(new Vector2(-m_WallJumpForce, m_JumpForce));
                //m_RigidBody2D.velocity = new Vector3(-60, 20);
                ParabolaJump(transform.position + new Vector3(-5, 5, 0), 3, m_WallJumpTime); //(5, 3f, 0), 3, m_wallJumpTime

                //StartCoroutine(LimitWallJumpMoveRight());
            }
        }

    }
   

    void JumpGravity(bool jump)
    {

        if (jump && m_AirJumpsLeft >= 1)
        {

           m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 0);
        }

        if (m_RigidBody2D.velocity.y < 0 && !m_OnWall) //we are falling
        {
            m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
        }
        else if(m_RigidBody2D.velocity.y < 0 && m_OnWall)
        {
            m_RigidBody2D.velocity = Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime * 10;
            
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

    IEnumerator LimitWallJumpMoveLeft()
    {

        yield return new WaitForSeconds(.6f);
        
        m_limitLeftMove = false;
    }

    IEnumerator LimitWallJumpMoveRight()
    {
        
        yield return new WaitForSeconds(.6f);
        m_limitRightMove = false;
    }

    public void Dash()
    {
        if (!m_Grounded)
        {

            if(m_FacingRight && m_DashLeft == 1)
            {
                m_RigidBody2D.AddForce(Vector3.right * m_DashForce * 100 );
                m_DashLeft--;
            }
            else if(!m_FacingRight && m_DashLeft == 1)
            {
                m_RigidBody2D.AddForce(Vector3.right * m_DashForce * -100);
                m_DashLeft--;
            }
        }
        else
        {
            if(m_FacingRight && m_GroundDash)
            {
                m_GroundDash = false;
                m_RigidBody2D.AddForce(Vector3.right * m_DashForce * 100);
            }
            else if(!m_FacingRight && m_GroundDash)
            {
                m_GroundDash = false;
                m_RigidBody2D.AddForce(Vector3.right * m_DashForce * -100);
            }
            StartCoroutine(GroundDashCooldown());
        }
    }

    IEnumerator GroundDashCooldown()
    {

        yield return new WaitForSeconds(3f);
        m_GroundDash = true;
    }


    public void ParabolaJump(Vector3 destination, float maxHeight, float time)
    {
        if (activeJumpCoroutine != null)
        {
            StopCoroutine(activeJumpCoroutine);
            activeJumpCoroutine = null;
            JumpProgress = 0.0f;
        }
        activeJumpCoroutine = JumpCoroutine(destination, maxHeight, time);
        StartCoroutine(activeJumpCoroutine);
    }

    private IEnumerator JumpCoroutine(Vector3 destination, float maxHeight, float time)
    {
        doingWallJump = true;
        bool jumped = false;
        var startPos = transform.position;
        while (JumpProgress <= 1.0 && !jumped)
        {
            if (Input.GetButtonDown("Jump") && m_AirJumpsLeft == 1)
            {
                doingWallJump = false;
                jumped = true;
            }
            JumpProgress += Time.deltaTime / time;
            var height = Mathf.Sin(Mathf.PI * JumpProgress) * maxHeight;

            if(height < 0f)
            {
                height = 0f;
            }
            //transform.position = Vector3.Lerp(startPos, destination, JumpProgress) + Vector3.up * height;
            m_RigidBody2D.MovePosition(Vector3.Lerp(startPos, destination, JumpProgress) + Vector3.up * height);
            yield return null;
        }
        doingWallJump = false;
        if (!jumped)
        {
            m_RigidBody2D.velocity = new Vector3(10, -15);
        }

        else if (jumped)
        {

            StartCoroutine(Delay());
        }
        //m_RigidBody2D.velocity = new Vector3(20, -15);
        //m_RigidBody2D.velocity = Vector3.Lerp(m_RigidBody2D.velocity, new Vector3(0, 0, 0) ,50f);//20, -15
        //m_RigidBody2D.AddForce(new Vector2(600, -600));

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.01f);
        m_RigidBody2D.velocity = new Vector2(0, 0);
        m_RigidBody2D.AddForce(new Vector2(0f, m_JumpForce));
        m_AirJumpsLeft--;
        m_DashLeft = 1;
    }
    
    IEnumerator WallDeadTime()
    {
        yield return new WaitForSeconds(.1f);
        wallDeadTime = false;
    }

  
}
