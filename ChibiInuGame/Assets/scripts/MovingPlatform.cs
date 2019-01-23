using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [Header("Init")]
    public GameObject platform;
    public List<Transform> positions;

    [Space]
    [Header("Stats")]
    public float platformVelocity;
    //public float movementTime;

    private int nextPosition;

    void Start()
    {
        //ChangeTarget();
    }

    void Update()
    {
        if (Vector2.Distance(platform.transform.position, positions[nextPosition].position) <= 2)
        {
            nextPosition = (nextPosition + 1) % positions.Count;
        }

        platform.transform.position = Vector2.MoveTowards(platform.transform.position,
            positions[nextPosition].position, platformVelocity * Time.deltaTime);
        //movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, platformVelocity * Time.deltaTime);
    }

    /*
    void ChangeTarget()
    {
        if (state == "Move1")
        {
            state = "Move2";
            newPosition = position2.position;
        }
        else if (state == "Move2")
        {
            state = "Move1";
            newPosition = position1.position;
        }
        else if (state == "")
        {
            state = "Move2";
            newPosition = position2.position;
        }
        Invoke("ChangeTarget", movementTime);
    }*/
}
