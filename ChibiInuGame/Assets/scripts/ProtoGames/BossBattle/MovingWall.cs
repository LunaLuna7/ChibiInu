using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour {

    [Header("Wall locations")]
    public Transform platform;
    public Transform positionA;
    public Transform positionB;
    public Vector3 newPosition;

    [Space]

    [Header("Walls Attributes")]
    public string state;
    public float smooth;
    public float movementTime;

    void Start()
    {
        ChangeTarget();
    }

    void FixedUpdate()
    {
        platform.position = Vector3.Lerp(platform.position, newPosition, smooth * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (state == "Move1")
        {
            state = "Move2";
            newPosition = positionB.position;
        }
        else if (state == "Move2")
        {
            state = "Move1";
            newPosition = positionA.position;
        }
        else if (state == "")
        {
            state = "Move2";
            newPosition = positionB.position;
        }
        Invoke("ChangeTarget", movementTime);
    }
    /*
    void moveLeft()
    {
        transform.position += Vector3.left * horizontalSpd * Time.deltaTime;
    }*/
}
