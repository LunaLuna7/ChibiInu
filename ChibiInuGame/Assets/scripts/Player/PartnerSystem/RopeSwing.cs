using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour {
    public HingeJoint2D hingeJoint;
    JointMotor2D newMotor;
    JointAngleLimits2D limits;
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
    public bool Off = false;
    public bool grabbable = true;

    // Use this for initialization
    void Start()
    {
        characterController2D = player.GetComponent<CharacterController2D>();
        hingeJoint = GetComponent<HingeJoint2D>();
        newMotor = hingeJoint.motor;
        limits = hingeJoint.limits;
        anchor = new Vector3(hingeJoint.anchor.x * transform.localScale.x, hingeJoint.anchor.y * transform.localScale.y, 0) + transform.position;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController2D.m_OnSwing)
        {
            playerCol.transform.position = anchor + (2 * (transform.position - anchor));
            playerCol.transform.rotation = transform.rotation;
            if (Input.GetKeyDown(detachKey))
            {
                Off = true;
                StartCoroutine(DelayLetGo());

            }
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
        if (collision.tag == "Player")
        {
            playerCol = collision;
            rb = playerCol.transform.GetComponent<Rigidbody2D>();
            characterController2D.m_OnSwing = true;
        }

    }

    public void LetGo()
    {
        if (!characterController2D.m_OnSwing)
            return;


        float currentAngle = playerCol.transform.rotation.eulerAngles.z;
        Vector3 detachDir = new Vector2();

        //checking edge cases 1st
        if (Mathf.Abs(currentAngle - 70) < 5)
            detachDir = playerCol.transform.rotation * Vector2.right;

        else if (Mathf.Abs(currentAngle - 290) < 5)
            detachDir = playerCol.transform.rotation * Vector2.left;

        else if (playerCol.transform.position != prevprevPosition)
        {
            detachDir = playerCol.transform.position - prevprevPosition;
        }
        else
        {
            Debug.Log("RopeSwing gone wrong");
        }

        playerCol.transform.rotation = new Quaternion();

        for (int i = 0; i < 5; ++i)
            rb.transform.Translate(detachDir.normalized * Time.deltaTime);

        rb.velocity = new Vector3();
        rb.AddForce(detachDir.normalized * detachPower, ForceMode2D.Impulse);
        Debug.Log(detachPower);
        characterController2D.m_OnSwing = false;
        characterController2D.m_AirJumpsLeft += 1;
        grabbable = false;
        this.gameObject.SetActive(false);
}


    private void OnEnable()
    {
        Off = false;
        player.transform.position = anchor + (2 * (transform.position - anchor));
        characterController2D.m_OnSwing = true;
        StartCoroutine(Swing());
        limits.min = -70;//290
        limits.max = 70;//430
        hingeJoint.limits = limits;
    }

    private void OnDisable()
    {
        Off = true;
    }

    IEnumerator DelayLetGo()
    {
        yield return new WaitForSeconds(.2f);
        LetGo();
    }

}
