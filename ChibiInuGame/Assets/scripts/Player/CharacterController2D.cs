using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    [SerializeField] private float m_JumpForce = 800f;
    [SerializeField] private int m_AirJumps = 0;
    [SerializeField] private float m_FallGravity = 4f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_HorizontalCheck;
    [SerializeField] private bool m_AirControl = false;


    private Rigidbody2D m_RigidBody2D;
    private bool m_Grounded;
    bool m_FacingRight = true;
    private bool m_OnJumpPad = false;
    private int m_AirJumpsLeft;
    private Vector3 m_Velocity = Vector3.zero;
    private float maxWalkAngel = 85f;


    void Awake () {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer);
        if (m_Grounded)
        {
            JumpadOff();
            m_AirJumpsLeft = m_AirJumps;
        }
    }

    public void Move(float move, bool jump)
    {

       
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

        if (m_Grounded && jump)
        {
            m_Grounded = false;
            m_RigidBody2D.AddForce(new Vector2(0f, m_JumpForce));
        }

        //air Jump
        else if (jump && m_AirJumpsLeft > 0)
        {
            m_Grounded = false;
            m_RigidBody2D.AddForce(new Vector2(0f,  m_JumpForce));
            m_AirJumpsLeft--;
        }

    }

    void JumpGravity(bool jump)
    {

        if (jump && m_AirJumpsLeft >= 1)
        {
            m_RigidBody2D.velocity = Vector2.zero;
        }

        if (m_RigidBody2D.velocity.y < 0) //we are falling
        {
            m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
        }
        else if (m_RigidBody2D.velocity.y > 0 && !Input.GetButton("Jump"))//tab jump
        {
            m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
        }
        else if (m_RigidBody2D.velocity.y > 0 && Input.GetButton("Jump") && m_OnJumpPad)
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
        if (collide.gameObject.tag == "hurtBox")
        {
            //to kill enemy, we tell the enemy script
            //StateController script = collide.gameObject.GetComponentInParent<StateController>();
            m_RigidBody2D.velocity = new Vector2(0, 10);
            //script.Die();
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
}
