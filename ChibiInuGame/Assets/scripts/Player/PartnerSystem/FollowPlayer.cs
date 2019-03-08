using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add the flip for partner
//Make a better follow mechanic

public class FollowPlayer : MonoBehaviour {

    [Header("Stats")]
    public float closeSpeed = 10f;
    public float farSpeed = 30f;
    public float distanceToSwitch = 20f;

    public GameObject player;
    private CharacterController2D playerController;
    private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
        playerController = player.GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {


        /*
        if (playerController.m_FacingRight)
        {
            if(Mathf.Abs(transform.position.x - player.transform.position.x) >= distanceToSwitch || Mathf.Abs(transform.position.y - player.transform.position.y) >= distanceToSwitch)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, farSpeed * Time.deltaTime);
            }
            else
            {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, closeSpeed * Time.deltaTime);

            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) >= distanceToSwitch || Mathf.Abs(transform.position.y - player.transform.position.y) >= distanceToSwitch)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(7f,0,0), farSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(7f, 0, 0), closeSpeed * Time.deltaTime);
            }
        }*/
	}
}
