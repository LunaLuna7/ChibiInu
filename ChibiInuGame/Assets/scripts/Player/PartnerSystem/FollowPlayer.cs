using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    private CharacterController2D playerController;
	// Use this for initialization
	void Start () {
        playerController = player.GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (playerController.m_FacingRight)
        {
            if(Mathf.Abs(transform.position.x - player.transform.position.x) >= 9 || Mathf.Abs(transform.position.y - player.transform.position.y) >= 9)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 30f * Time.deltaTime);
            }
            else
            {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10f * Time.deltaTime);

            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) >= 9 || Mathf.Abs(transform.position.y - player.transform.position.y) >= 9)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(7f,0,0), 30f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(7f, 0, 0), 10f * Time.deltaTime);
            }
        }
	}
}
