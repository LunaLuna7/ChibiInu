using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardBoss : MonoBehaviour {

    public EnemyStats enemyStats;
    [HideInInspector] public GameObject player;

    public List<Transform> patrolLocations;

    public int action;
    public float timer;
    public float minTime;
    public float maxTime;

    public GameObject spikesLeft;
    public GameObject spikesRight;

    private bool inState;
    private bool ActionCompleted;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = Random.Range(minTime, maxTime);
        inState = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!inState)
        {

            Idle();
            if (timer <= 0)
            {
                inState = true;
                action = Random.Range(0, 1);
                Act(action);
            }
            else
                timer -= Time.deltaTime;
        }
        else{
            //doing an action
        }
        
    }

    private void Idle()
    {
        
    }

    private void Act(int act)
    {
        if(act == 0)
        {
            Spikes();
            StartCoroutine(ActionDone(7f));
        }
    }

    private void RushTowards()
    {
        Debug.Log("OK");
        if(transform.position.x != patrolLocations[0].position.x || transform.position.x != patrolLocations[1].position.x)
        {
            if (transform.position.x > player.transform.position.x) //Player is on the left
            {
                transform.position = Vector2.MoveTowards(transform.position,
                patrolLocations[0].position, enemyStats.moveSpeed * Time.deltaTime);
                Debug.Log("HMMMM");
            }
            else if (transform.position.x <= player.transform.position.x)//Player is on the right
            {
                Debug.Log("WOW");
                transform.position = Vector2.MoveTowards(transform.position,
               patrolLocations[1].position, enemyStats.moveSpeed * Time.deltaTime);
            }

        }
        
    }

    private void HorizontalProjectiles()
    {

    }
   
    private void Spikes()
    {
        spikesLeft.SetActive(true);
        spikesRight.SetActive(true);
    }

    IEnumerator ActionDone(float actionDuration)
    {
        yield return new WaitForSeconds(actionDuration);
        Debug.Log("ActionDone");
        timer = Random.Range(minTime, maxTime);
        inState = false;
    }
}
