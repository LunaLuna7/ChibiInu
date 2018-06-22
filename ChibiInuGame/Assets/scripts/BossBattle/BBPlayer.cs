using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBPlayer : MonoBehaviour {

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

    private bool inDamageArea;

    //skills
    public GameObject blackHole;

    // Use this for initialization
    void Start()
    {
        originalSpeed = speed;
        movingUp = true;
        movingDown = false;
        movingRight = false;
        movingLeft = false;
        inDamageArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        Skills();
        UpdateDirection();
        ConstantMove();
    }

    void ConstantMove()
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

    void Skills()
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
            Instantiate(blackHole, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Skill in D");
            StartCoroutine(HardenSkill());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "AOE")
        {
            inDamageArea = true;
            StartCoroutine(DamageOverTime());
        }

        if (collision.gameObject.tag == "attack" && immune == false)
        {
            HP--;
        }

        if (HP <= 0)
        {
            Debug.Log("you die");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AOE")
        {
            inDamageArea = false;
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

    IEnumerator DamageOverTime()
    {
        while (inDamageArea)
        {
            HP--;
            yield return new WaitForSeconds(2);
        }
    }

}
