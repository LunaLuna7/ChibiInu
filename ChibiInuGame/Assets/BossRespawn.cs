using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRespawn : MonoBehaviour {

    public Transform startPosition;
    public PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerHealth.HPLeft == 0)
        {
            ResetBoss();
        }
	}


    public void ResetBoss()
    {
        transform.position = startPosition.position;
    }
}
