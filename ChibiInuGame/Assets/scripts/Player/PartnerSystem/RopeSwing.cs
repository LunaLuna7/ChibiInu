using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour {

    public HingeJoint2D hingeJoint;
    JointMotor2D newMotor;
    public float swingTime;
    public float detachPower;
    public float attachCooldownTime;
    Collider2D playerCol;
    Vector3 anchor;
    Rigidbody2D rb;
    Vector3 prevprevPosition;
    Vector3 prevPosition;
    public KeyCode detachKey;
    private CharacterController2D characterController2D;
    public GameObject player;

    [Header("States")]
    private bool grabbable;

	// Use this for initialization
	void Start () {
        characterController2D = player.GetComponent<CharacterController2D>();
        hingeJoint = GetComponent<HingeJoint2D>();
        newMotor = hingeJoint.motor;
        StartCoroutine(Swing());
        anchor = new Vector3(hingeJoint.anchor.x * transform.localScale.x, hingeJoint.anchor.y * transform.localScale.y, 0)   +   transform.position;

        grabbable = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (characterController2D.m_OnSwing)
        {
            playerCol.transform.position = anchor + (2 * (transform.position - anchor));
            playerCol.transform.rotation = transform.rotation;
            if(Input.GetKeyDown(detachKey))
                LetGo();
            prevprevPosition = prevPosition;
            prevPosition = playerCol.transform.position;
        }

        

    }

    public void SwitchMotorSpeed()
    {
        newMotor.motorSpeed = newMotor.motorSpeed * -1;
        hingeJoint.motor = newMotor;
    }

    IEnumerator Swing()
    {
        while (true)
        {
           
            yield return new WaitForSeconds(swingTime);
            SwitchMotorSpeed();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && grabbable)
        {
            playerCol = collision;
            rb = playerCol.transform.GetComponent<Rigidbody2D>();
            characterController2D.m_OnSwing = true;
            grabbable = false;
            
        }
        
    }

    public void LetGo()
    {
        if (!characterController2D.m_OnSwing)
            return;


        float currentAngle = playerCol.transform.rotation.eulerAngles.z;
        Vector3 detachDir = new Vector2();
        
        //checking edge cases 1st
        if (Mathf.Abs(currentAngle - 70) < 5){
           
            detachDir = playerCol.transform.rotation * Vector2.right;
            //Debug.Log("\t R " + detachDir);
        }

        else if(Mathf.Abs(currentAngle - 290) < 5)
        {
            detachDir = playerCol.transform.rotation * Vector2.left;    
            //Debug.Log("\t L " + detachDir.ToString());
        }

        else if(playerCol.transform.position != prevprevPosition)
        {
            //Vector3 a = new Vector3(-playerCol.transform.position.x + prevprevPosition.x, 10, 0);
            detachDir = playerCol.transform.position - prevprevPosition;
            //detachDir = a;
            //Debug.Log(detachDir);
        }
        else
        {
            //Debug.Log(prevprevPosition);
            //Debug.Log(playerCol.transform.position);
            Debug.Log("RopeSwing gone wrong");
        }

        Debug.Log("Adding Force");
        playerCol.transform.rotation = new Quaternion();

        for(int i = 0; i < 5; ++i)
            rb.transform.Translate(detachDir.normalized * Time.deltaTime);
        
        rb.velocity = new Vector3();
        rb.AddForce( detachDir.normalized * detachPower , ForceMode2D.Impulse);
        characterController2D.m_OnSwing = false;
        StartCoroutine(GrabDelay());
        characterController2D.m_AirJumpsLeft += 1;
    }

    IEnumerator GrabDelay()
    {
        yield return new WaitForSeconds(attachCooldownTime);
        grabbable = true;
    }
}
