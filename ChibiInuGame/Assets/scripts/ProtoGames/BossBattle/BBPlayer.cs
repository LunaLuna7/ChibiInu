using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBPlayer : MonoBehaviour {

    [Header("Player attributes")]
    public float HP;
    private float maxHP; 
    [SerializeField]
    private float speed;
    private float originalSpeed;
    public bool immune;

    public Image healthBar;
    public Text healthBarTxt;

    //bools that keep track of ChibiInu direction
    private bool movingUp;
    private bool movingDown;
    private bool movingRight;
    private bool movingLeft;

    private bool inDamageArea;

    //skills
    public GameObject blackHole;

    //Cooldown skills
    public bool dashOn;
    public float dashCoolDown;

    private bool hardenOn;
    private bool holeOn;
    

    // Use this for initialization
    void Start()
    {
        healthBarTxt.text = "100%";
        maxHP = HP;
        originalSpeed = speed;
        movingUp = false;
        movingDown = false;
        movingRight = false;
        movingLeft = false;
        inDamageArea = false;

        //coolDown reset
        dashOn = false;
        hardenOn = false;
        holeOn = false;
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
        if (Input.GetKeyDown(KeyCode.S) && dashOn == false)
        {
            Debug.Log("Skill in S");
            StartCoroutine(DashSkill());
            //speed = originalSpeed;
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
            //speed = originalSpeed;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "AOE")
        {
            inDamageArea = true;
            StartCoroutine(DamageOverTime());
        }

        else if (collision.gameObject.tag == "attack" && immune == false)
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
            StopAllCoroutines();
        }
    }


    IEnumerator DashSkill()
    {
        dashOn = true;
        speed = 40;
        yield return new WaitForSeconds(.07f);
        speed = 4;
        StartCoroutine(DashInactiveWait());
        
    }


    IEnumerator HardenSkill()
    {
        speed = 0;
        immune = true;
        yield return new WaitForSeconds(1.5f);
        speed = originalSpeed;
        immune = false;
    }


    IEnumerator DamageOverTime()
    {
        while (inDamageArea)
        {
            HP--;
            float currentHP = HP / maxHP;
            healthBar.fillAmount = currentHP;
            healthBarTxt.text = (currentHP * 100).ToString() + "%";
            yield return new WaitForSeconds(2);
        }
    }


    IEnumerator DashInactiveWait()
    {
        yield return new WaitForSeconds(dashCoolDown);
        dashOn = false;
    }

}
