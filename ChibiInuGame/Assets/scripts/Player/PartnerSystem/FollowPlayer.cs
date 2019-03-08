using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add the flip for partner
//Make a better follow mechanic

public class FollowPlayer : MonoBehaviour {

    [Header("Stats")]
    public float closeSpeed = 20f;
    public float distanceToTeleport = 35f;
    [Space][Space]

    [Header("Components")]
    public GameObject player;
    public List<GameObject> partnerSlots;
    private CharacterController2D playerController;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        playerController = player.GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () { 
        FacePlayer();
        
	}

    private void FixedUpdate()
    {
        FollowThePlayer();
    }

    void FollowThePlayer()
    {
         if (playerController.m_FacingRight)
         {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) >= distanceToTeleport || Mathf.Abs(transform.position.y - player.transform.position.y) < distanceToTeleport)
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, closeSpeed * Time.deltaTime);
                
            else
                transform.position = player.transform.position;   
         }
         else
         {
             if (Mathf.Abs(transform.position.x - player.transform.position.x) >= distanceToTeleport || Mathf.Abs(transform.position.y - player.transform.position.y) < distanceToTeleport)
                 transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(7f, 0, 0), closeSpeed * Time.deltaTime);
            else
                transform.position = player.transform.position;
         }
    }

    //Loops through all the PartnerSlots and check their position in respect to player. Then change scale to flip them
    void FacePlayer()
    {
        
        foreach(GameObject partner in partnerSlots)
        {

            Vector2 localScale = partner.transform.localScale;
            if (partner.transform.position.x - player.transform.position.x > 0)
                localScale.x = -1;
            else
                localScale.x = 1;
            partner.transform.localScale = localScale;
        }
    }
}
