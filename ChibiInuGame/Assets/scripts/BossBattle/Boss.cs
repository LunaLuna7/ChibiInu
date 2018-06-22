using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public List<BossAttack> bossAttack;
    public Image healthBar;
    public Text healthBarTxt;

    [Header("Stats")]
    public float startHealth;
    public float health;
    public int spawnWait;
    [Space]

    [Header("Movement")]
    [Tooltip("How much enemy moves every time")]
    public float horizontalSpd;
    [Tooltip("How often enemy moves")]
    public float horizontalMoveWait;

    public GameObject dialogueCanvas;
    public Text dialogue;

    [Space]
    private int Lcount; //<-Makes sure enemy doesn't move out of scope
    private int Rcount;
    private bool skipNextAtk = false;



    void Start()
    {
        healthBarTxt.text = "100%";
        health = startHealth;
        InvokeRepeating("triggerAtk", 2f, spawnWait); //(function name, wait seconds since Start, seconds between calls)
        InvokeRepeating("horizontalMove", 2f, horizontalMoveWait);
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    void triggerAtk()
    {
        int randomAtk = Random.Range(0, bossAttack.Count);


        if (!skipNextAtk)
        {
            dialogue.text = bossAttack[randomAtk].attackWarning;
            StopAllCoroutines();
            StartCoroutine(ActivateAttack(randomAtk, bossAttack[randomAtk].warningWait));
        }
        if (!bossAttack[randomAtk].isNormal)
        {
            skipNextAtk = true;
        }
        else
        {
            skipNextAtk = false;
        }
    }
    

    void moveLeft()
    {
        transform.position += Vector3.left * horizontalSpd * Time.deltaTime;
    }
    void moveRight()
    {
        transform.position += Vector3.right * horizontalSpd * Time.deltaTime;
    }

    void horizontalMove()
    {
        int randomInt = Random.Range(0, 2);

        if (Lcount > 3 && randomInt == 0)
            randomInt = 1;

        else if (Rcount > 3 && randomInt == 1)
            randomInt = 0;

        if (randomInt == 0)
        {
            moveLeft();
            Lcount++;
            Rcount--;
        }
        else
        {
            moveRight();
            Lcount--;
            Rcount++;
        }
    }

    public void HitBoss()
    {
        health--;
        float currentHP = health / startHealth;
        healthBar.fillAmount = currentHP;
        healthBarTxt.text = (currentHP * 100).ToString() + "%";
        if(health <= 0)
        {
            DestroyObject(gameObject);
        }
    }

    IEnumerator ActivateAttack(int atk, float waitTime)
    {
        dialogueCanvas.SetActive(true);
        yield return new WaitForSeconds(bossAttack[atk].warningWait);
        dialogueCanvas.SetActive(false);
        Instantiate(bossAttack[atk].attack, this.transform.position, this.transform.rotation);
    }
}
