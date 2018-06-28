using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakePlayer : MonoBehaviour {

    [Header("Player attributes")]
    public float HP;
    private float maxHP;
    public float speed;
    private float originalSpeed;
    public bool immune;

    public Transform temp;

    //public Image healthBar;
    //public Text healthBarTxt;

    //bools that keep track of ChibiInu direction
    private bool movingUp;
    private bool movingDown;
    private bool movingRight;
    private bool movingLeft;

    private void Start()
    {
        //healthBarTxt.text = "100%";
        maxHP = HP;
        originalSpeed = speed;
        movingUp = false;
        movingDown = false;
        movingRight = false;
        movingLeft = false;
    }

    void Update()
    {
        UpdateDirection();
        ConstantMove();
        //Follow();
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack" && immune == false)
        {
            HP--;
            float currentHP = HP / maxHP;
            healthBar.fillAmount = currentHP;
            healthBarTxt.text = (currentHP * 100).ToString() + "%";
        }

        else if (collision.gameObject.tag == "Boarder")
        {
            HP = 0;
            float currentHP = HP / maxHP;
            healthBar.fillAmount = currentHP;
            healthBarTxt.text = (currentHP * 100).ToString() + "%";
        }

        else if(collision.gameObject.tag == "food")
        {
            speed++;
        }

        if (HP <= 0)
        {
            Debug.Log("you die");
        }
    }
    */
    void ConstantMove()
    {
        if (movingLeft)
        {
            temp = transform;
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

    void UpdateDirection()
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

   
}
