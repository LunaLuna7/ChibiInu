using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [Header("Attack Properties")]

    public GameObject atk1;
    public int atk1Cooldown;
    [Range(0, 10)]
    public float atkWaveWait;

    [Space]

    [Header("Movement")]
    [Tooltip("How much enemy moves every time")]
    public float horizontalSpd;
    [Tooltip("How often enemy moves")]
    public float horizontalMoveWait;

    [Space]

    [SerializeField]
    private int Lcount; //<-Makes sure enemy doesn't move out of scope
    [SerializeField]
    private int Rcount;


    void Start()
    {
        InvokeRepeating("triggerAtk1", 2f, atkWaveWait); //(function name, wait seconds since Start, seconds between calls)
        InvokeRepeating("horizontalMove", 2f, horizontalMoveWait);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void triggerAtk1()
    {
        Instantiate(atk1, this.transform.position, this.transform.rotation);
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

}
