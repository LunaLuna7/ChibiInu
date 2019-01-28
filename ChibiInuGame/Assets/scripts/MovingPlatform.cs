using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [Header("Init")]
    public GameObject platform;
    public List<Transform> positions;
    public bool moveOnTouch;
    private Platform platformChild;
    [SerializeField]private PlayerHealth playerHealth;


    [Space]
    [Header("Stats")]
    public float platformVelocity;
    private int nextPosition;
   


    void Start()
    {
        platformChild = platform.GetComponent<Platform>();
    }

    void Update()
    {
        

        if (!moveOnTouch || platformChild.touchedPlayer)
        {
            if (Vector2.Distance(platform.transform.position, positions[nextPosition].position) <= 2)
                nextPosition = (nextPosition + 1) % positions.Count;
        
            platform.transform.position = Vector2.MoveTowards(platform.transform.position,
                positions[nextPosition].position, platformVelocity * Time.deltaTime);
        }

        if(moveOnTouch && playerHealth.HPLeft <= 0)
        {
            ResetPlatform();
        } 
       
    }

    private void ResetPlatform()
    {
        platformChild.touchedPlayer = false;
        platform.transform.position = positions[0].position;
    }
}
