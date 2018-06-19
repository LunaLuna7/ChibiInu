using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public List<BossAttack> bossAttack;

    [Header("Stats")]
    public int health;
    public int spawnWait;
    [Space]

    [Header("Movement")]
    [Tooltip("How much enemy moves every time")]
    public float horizontalSpd;
    [Tooltip("How often enemy moves")]
    public float horizontalMoveWait;

    public GameObject bubble;
    public ClampName clampName;

    [Space]

    [SerializeField]
    private int Lcount; //<-Makes sure enemy doesn't move out of scope
    [SerializeField]
    private int Rcount;
    [SerializeField]
    private bool skipNextAtk = false;


    void Start()
    {
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
            clampName.Message(bossAttack[randomAtk].attackWarning);
            clampName.ActiveMessage(bossAttack[randomAtk].isNormal, true);
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
        clampName.UpdateTalkPosition();
    }
    void moveRight()
    {
        transform.position += Vector3.right * horizontalSpd * Time.deltaTime;
        clampName.UpdateTalkPosition();
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
        if(health >= 0)
        {
            DestroyObject(gameObject);
        }
    }

    IEnumerator ActivateAttack(int atk, float waitTime)
    {
        yield return new WaitForSeconds(bossAttack[atk].warningWait);
        Instantiate(bossAttack[atk].attack, this.transform.position, this.transform.rotation);
    }
}
