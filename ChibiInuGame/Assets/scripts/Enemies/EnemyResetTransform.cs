using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reset the transform of the enemy after player or enemey dies
public class EnemyResetTransform : MonoBehaviour {
	private Vector3 startPosition;
	public GameObject enemyObject;
	private StateController enemyStateController;
    //private StateController temp;
	
    // Use this for initialization
	void Awake()
	{
        //temp = GetComponent<StateController>();
		startPosition = transform.position;
		enemyStateController = enemyObject.GetComponent<StateController>();
	}
	
	// Update is called once per frame
	void Update () {
		//reset position if enemy/player dies
		if((enemyStateController.killed || enemyStateController.playerHealth.HPLeft <= 0))
		{
            enemyStateController.killed = false;
            enemyStateController.playerInRange = false;
            //temp.health = temp.enemyStats.HP;
            enemyObject.transform.position = startPosition;
			//enemyObject.SetActive(false);
		}
	}
}
