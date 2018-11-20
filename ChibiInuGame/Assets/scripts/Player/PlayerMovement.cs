using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float runSpeed;

    float horizontalMove = 0f;
    bool jump = false;
    public CharacterController2D controller;


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        
	}

    private void FixedUpdate()
    {
        if (!controller.m_Paralyzed)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;

        }
        else
        {
            controller.Move(0, false);
        }
    }

    
}
