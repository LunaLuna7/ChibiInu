using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cinemachine;

public class CharacterController2D : MonoBehaviour {

    [Header("Stats")]
    private float m_JumpForce = 1300f;
    [SerializeField] 
    private float groundJumpPower = 1300f;
    public float waterJumpPower = 1000f;
    [SerializeField] private float m_WallJumpTime = .5f;
    [SerializeField] public int m_AirJumps = 0;
    private float m_FallGravity = 4f;
    [SerializeField] private float m_fallGravityLand = 7f;
    [SerializeField] private float m_FallGavityWater;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private LayerMask m_WallLayer;
    [SerializeField] private LayerMask m_WaterLayer;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_GroundCheck2;
    [SerializeField] private Transform m_WallCheck;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private float m_DashForce = 1000f;
    public bool paralyzedWhenStart = false;

    [Header("States")]
    public bool m_limitRightMove;
    public bool m_limitLeftMove;
    public bool m_FacingRight = true;
    public bool m_Grounded;
    public bool m_Swimming;
    public bool m_Paralyzed;
    private bool doingWallJump = false;
    public bool m_Immune = false;
    public bool m_Damaged;
    private bool m_OnWall;
    public bool m_OnDash;
    public bool m_OnJumpPad = false;
    public bool m_OnOtherCamera = false;
    public bool m_OnShield;

    public int m_AirJumpsLeft;
    public int m_DashLeft;
    private bool wallDeadTime;
    public bool m_godMode;

    public Camera cam;
    public PlayerHealth playerHealth;
    [HideInInspector] public Rigidbody2D m_RigidBody2D;
    public Animator anim;
    public Transform fireSpawn;
    private Vector3 m_Velocity = Vector3.zero;
    SpriteRenderer playerSprite;
    public GameObject DashParticle;
    public GameObject DashSpawner;
    public UIPartnerBook uIPartnerBook;
    public GameObject playerGameObject;
    public GameObject shields;
    public GameObject godModeParticles;

    float horizontalMove;

    //SoundStates
    private bool PlayingWallSlide;
    private bool walkingSound;
    private bool swimmingAnimOn;

    private IEnumerator activeJumpCoroutine;
    public float JumpProgress { get; private set; }
    bool goBackToZero;
   

    void Awake () {
        m_Paralyzed = paralyzedWhenStart;
        goBackToZero = false;
        PlayingWallSlide = false;
        swimmingAnimOn = false;
        m_DashLeft = 1;
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        //soundEffectManager = FindObjectOfType<SoundEffectManager>();
        anim = GetComponentInChildren<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        anim.SetBool("Grounded", m_Grounded);
        anim.SetBool("Swimming", m_Swimming);
        anim.SetInteger("MovingDir", (int)horizontalMove);
        anim.SetFloat("MovementSpeed", Mathf.Abs(m_RigidBody2D.velocity.x)/12f);

    }

    void FixedUpdate () {
        
        m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer) || Physics2D.Linecast(transform.position, m_GroundCheck2.position, m_GroundLayer);
        m_Swimming = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_WaterLayer);
        m_OnWall = Physics2D.OverlapCircle(m_WallCheck.position, .2f, m_WallLayer);


        if (m_Grounded || m_Swimming)
        {
            //JumpadOff();
            m_AirJumpsLeft = m_AirJumps;
            OffWallSound();    
        }

        if (m_Swimming)
        {
            if (!swimmingAnimOn)
            {
                anim.Play("ShibSwimming");
                swimmingAnimOn = true;
            }

            m_JumpForce = waterJumpPower;
            m_RigidBody2D.gravityScale = 2.5f;

            if (m_RigidBody2D.velocity.y < -5)
                    goBackToZero = true;

            if (goBackToZero)
               m_RigidBody2D.velocity = Vector3.Lerp(m_RigidBody2D.velocity, new Vector3(m_RigidBody2D.velocity.x, 0), 5f * Time.deltaTime);
            

            if (m_RigidBody2D.velocity.y == 0)
                goBackToZero = false;

        }
        

        else if (!m_Swimming)
        {
            swimmingAnimOn = false;
            if (!m_OnShield)
            {
                m_JumpForce = groundJumpPower;
                m_FallGravity = m_fallGravityLand;
                m_RigidBody2D.gravityScale = 5;
            }
        }

    }
    

    public void Move(float move, bool jump)
    {
        horizontalMove = move;
        //if (m_OnJumpPad && (m_RigidBody2D.velocity.y < 0 || jump))
                //m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, 0, 0);

        

        if (m_Grounded)
        {
            m_limitLeftMove = false;
            m_limitRightMove = false;

            if (playerHealth.HPLeft >= 1 && move != 0)
                PlayerFootStepSound();
           
        }
        
        if (m_limitRightMove || m_limitLeftMove)
            move = 0;

        if(m_OnDash)
        {
            if(m_FacingRight)
                m_RigidBody2D.velocity = new Vector2(50, 0);
            else
                m_RigidBody2D.velocity = new Vector2(-50, 0);
        }

        #region WallLogic

        if (m_OnWall)
        {
            if((move < 0  && m_FacingRight) && wallDeadTime || (move > 0 && !m_FacingRight) && wallDeadTime)
            {
                move = 0;
                StartCoroutine(WallDeadTime());
            }
        }
        else if (!m_OnWall)
            wallDeadTime = true;


        #endregion WallLogic

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

        if(!m_Swimming)
            JumpGravity(jump);

        #region Jump
        if (jump)
        {

            //Ground Jump
            if (m_Grounded && !m_OnDash)
            {
                swimmingAnimOn = false;
                anim.Play("ShibaAirJump");
                SoundEffectManager.instance.Play("Jump");
                if(!m_OnJumpPad)
                    m_RigidBody2D.AddForce(new Vector2(m_RigidBody2D.velocity.x, m_JumpForce));
                m_DashLeft = 1;
            }
        

            //Air Jump
            else if (!m_Grounded && m_AirJumpsLeft > 0 && !m_OnWall && !doingWallJump && !m_OnDash)
            {
                swimmingAnimOn = false;
                if (m_AirJumpsLeft >= 2 && !m_OnJumpPad)
                {
                    anim.Play("ShibaAirJump2");
                    SoundEffectManager.instance.Play("AirJump");

                }
                
                else if (m_AirJumpsLeft == 1 && !m_OnJumpPad)
                {
                    anim.Play("ShibaAirJump");
                    SoundEffectManager.instance.Play("AirJump");
                }

                if (m_Swimming)
                    m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, 0);

                if (!m_OnJumpPad)
                {
                    m_RigidBody2D.AddForce(new Vector2(0f,  m_JumpForce));
                    if(!m_godMode)
                        m_AirJumpsLeft--;
                }
                    m_DashLeft = 1;
            }
   

            //Wall Jump
            else if (!m_Grounded && m_OnWall && !m_OnDash)
            {
                m_DashLeft = 1;
                OffWallSound();
                SoundEffectManager.instance.Play("WallJump");
                Flip();

                if (m_FacingRight)
                    ParabolaJump(transform.position + new Vector3(5, 5, 0), 3, m_WallJumpTime);

                else if (!m_FacingRight)
                    ParabolaJump(transform.position + new Vector3(-5, 5, 0), 3, m_WallJumpTime);
            }
        }
        #endregion Jump
    }


    void JumpGravity(bool jump)
    {
            if (jump && m_AirJumpsLeft >= 1 && !m_OnJumpPad)
            {
                m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 0);
            }

            if (m_RigidBody2D.velocity.y < 0 && !m_OnWall) //we are falling
            {
                m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;
                playerGameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
            else if(m_RigidBody2D.velocity.y < 0 && m_OnWall)
            {
                m_RigidBody2D.velocity = Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime * 5;
                anim.Play("ShibWallJump");
                playerGameObject.transform.localPosition = new Vector3(.5f, 0, 0);
                //transform.rotation = new Quaternion(0, 0, 90, transform.rotation.w);
            }
            if (!PlayingWallSlide && m_OnWall)
            {
                OnWallSound();
            }

            
            else if ( (m_RigidBody2D.velocity.y > 0) && !Input.GetButton("Jump") && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow))
            {
                m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity -1) * Time.deltaTime;
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
            m_DashLeft = 1;
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
        m_AirJumpsLeft = m_AirJumps;
        StartCoroutine(JumpadOffOverTime());
    }
    public void JumpadOff()
    {
        m_OnJumpPad = false;
    }

    public IEnumerator JumpadOffOverTime()
    {
        yield return new WaitForSeconds(.25f);
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
        
        if (!m_Grounded && Time.timeScale != 0)
        {
          
            if (m_FacingRight && m_DashLeft == 1)
            {
                SoundEffectManager.instance.Play("Dash");
                anim.Play("ShibDash");
                StartCoroutine(PerformingDash());
                StartCoroutine(DashJuice());
                m_RigidBody2D.AddForce(Vector3.right * m_DashForce * 150 );
                m_DashLeft--;
            }
            else if(!m_FacingRight && m_DashLeft == 1)
            {
                SoundEffectManager.instance.Play("Dash");
                anim.Play("ShibDash");
                StartCoroutine(PerformingDash());
                StartCoroutine(DashJuice());
                m_RigidBody2D.AddForce(Vector3.right * m_DashForce * -150);
                m_DashLeft--;
            }
        }
        
    }

    //Methods to handle the shield skill
    public void TriggerShield()
    {
        if(Time.timeScale != 0)
        { 
            if (!m_OnShield)
                ShieldOn();
            else if (m_OnShield)
                ShieldOff();
        }
    }
    private void ShieldOn()
    {
        SoundEffectManager.instance.Play("ShieldOn");
        shields.SetActive(true);
        m_RigidBody2D.gravityScale = 10;
        m_Immune = true;
        m_OnShield = true;
    }

    public void FakeShieldOn()
    {
        m_Immune = true;
        shields.SetActive(true);
    }

    public void FakeShieldOff()
    {
        m_Immune = false;
        shields.SetActive(false);
    }

    public void ShieldOff()
    {
        shields.SetActive(false);
        //playerSprite.color = Color.white;
        m_RigidBody2D.gravityScale = 5;
        m_Immune = false;
        m_OnShield = false;
    }
 

    //==================================================================
    //Makes player follow a parabolic path base on destination Transform
    //==================================================================
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
        while (JumpProgress <= 1.0 && !jumped && !m_OnDash)
        {
            if (Input.GetButtonDown("Jump") && m_AirJumpsLeft >= 1)
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

    }

    //==============================================
    //Handles dash delay for ground dash(not being used
    //==============================================
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.01f);
        m_RigidBody2D.velocity = new Vector2(0, 0);
        m_RigidBody2D.AddForce(new Vector2(0f, m_JumpForce));
        m_DashLeft = 1;
    }

    //==================================================================
    //Allows players to move joystick and press Jump button to wall jump
    //==================================================================
    IEnumerator WallDeadTime()
    {
        yield return new WaitForSeconds(.1f);
        wallDeadTime = false;
    }

    //==================================================================
    //Keeps track of dashing state of the player
    //==================================================================
    IEnumerator PerformingDash()
    {
        m_OnDash = true;
        
        m_limitRightMove = true;
        m_limitLeftMove = true;
        yield return new WaitForSeconds(.2f);
        m_limitRightMove = false;
        m_limitLeftMove = false;
        m_OnDash = false;
        
    }
    IEnumerator DashJuice()
    {
        DashParticle.transform.transform.localScale = Vector3.one;
        DashParticle.transform.localPosition = Vector3.zero;
        DashParticle.transform.SetParent(null);
        DashParticle.SetActive(true);
        yield return new WaitForSeconds(.3f);
        DashParticle.SetActive(false);
        DashParticle.transform.SetParent(DashSpawner.transform);
        DashParticle.transform.localPosition = Vector3.zero;
        DashParticle.transform.transform.localScale = Vector3.one;
    }

    //=======================================================
    //Handles wall Sound duration depending on player's state
    //=======================================================
    private void OnWallSound()
    {

        SoundEffectManager.instance.Play("WallSlide");
        PlayingWallSlide = true;
    }

    private void OffWallSound()
    {
        PlayingWallSlide = false;
        SoundEffectManager.instance.Stop("WallSlide");
    }
  
    private void PlayerFootStepSound()
    {
        if (!walkingSound)
        {
            walkingSound = true;
            StartCoroutine(SoundStep());
        }
    }

    IEnumerator SoundStep()
    {
        int step = Random.Range(1, 5);
        SoundEffectManager.instance.Play("FootStep" + step);
        yield return new WaitForSeconds(.5f);
        walkingSound = false;
    }

    public void GodMode(bool active)
    {
        m_godMode = active;
        godModeParticles.SetActive(active);
    }
}
