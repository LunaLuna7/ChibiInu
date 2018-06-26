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

    private bool AOE;
    private bool bossAlive;


    void Start()
    {
        bossAlive = true;
        AOE = false;
        healthBarTxt.text = "100%";
        health = startHealth;
        StartCoroutine(Attack());
        InvokeRepeating("horizontalMove", 2f, horizontalMoveWait);
    }

    // Update is called once per frame
    void Update()
    {
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

    IEnumerator ActivateAttack(int atk)
    {
        if(bossAttack[atk].ID == "gas")
        {
            AOE = true;
            StartCoroutine(AllowAOE());
        }
        dialogueCanvas.SetActive(true);
        yield return new WaitForSeconds(bossAttack[atk].warningWait);
        dialogueCanvas.SetActive(false);
        for (int i = bossAttack[atk].repetitions; i > 0; --i)
        {
            Instantiate(bossAttack[atk].attack, bossAttack[atk].location.position, bossAttack[atk].location.rotation);
            yield return new WaitForSeconds(bossAttack[atk].spawnWait);
        }
    }

    IEnumerator Attack()
    {
        while (bossAlive)
        {
            int randomAtk = Random.Range(0, bossAttack.Count);
            if(bossAttack[randomAtk].ID == "gas" && AOE == true)
            {
                continue;
            }
            dialogue.text = bossAttack[randomAtk].attackWarning;
            StartCoroutine(ActivateAttack(randomAtk));
            Debug.Log("loop");
            yield return new WaitForSeconds(bossAttack[randomAtk].coolDown);
        }
    }

    IEnumerator AllowAOE()
    {
        yield return new WaitForSeconds(35);
        AOE = false;
    }
}
