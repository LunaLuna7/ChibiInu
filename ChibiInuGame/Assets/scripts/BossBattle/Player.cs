﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player attributes")]
    public int HP;
    [SerializeField]
    private float speed;
    private float originalSpeed;
    public bool immune;

    //bools that keep track of ChibiInu direction
    private bool movingUp;
    private bool movingDown;
    private bool movingRight;
    private bool movingLeft;



    // Use this for initialization
    void Start()
    {
        originalSpeed = speed;
        movingUp = true;
        movingDown = false;
        movingRight = false;
        movingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        skills();
        updateDirection();
        constantMove();
    }

    void constantMove()
    {
        if (movingLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (movingUp)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (movingDown)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

    }

    void updateDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movingUp = true;
            movingDown = false;
            movingRight = false;
            movingLeft = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movingUp = false;
            movingDown = true;
            movingRight = false;
            movingLeft = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movingUp = false;
            movingDown = false;
            movingRight = true;
            movingLeft = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movingUp = false;
            movingDown = false;
            movingRight = false;
            movingLeft = true;
        }
    }

    void skills()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Skill in W");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Skill in S");
            StartCoroutine(DashSkill());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Skill in A");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Skill in D");
            StartCoroutine(HardenSkill());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "attack" && immune == false)
        {
            HP--;
        }

        if(HP <= 0)
        {
            Debug.Log("you die");
        }
    }

    IEnumerator DashSkill()
    {
        speed *= 10;
        yield return new WaitForSeconds(.02f);
        speed /= 10;
    }

    IEnumerator HardenSkill()
    {
        speed = 0;
        immune = true;
        yield return new WaitForSeconds(1f);
        speed = originalSpeed;
        immune = false;
    }

}
