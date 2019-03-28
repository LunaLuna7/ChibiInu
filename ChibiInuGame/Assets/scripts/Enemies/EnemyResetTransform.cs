using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reset the transform of the enemy after player or enemey dies
public class EnemyResetTransform : MonoBehaviour {
	private Vector3 startPosition;
	public GameObject enemyObject;
	private StateController enemyStateController;
	// Use this for initialization
	void Awake()
	{
		startPosition = transform.position;
		enemyStateController = enemyObject.GetComponent<StateController>();
	}
	
	// Update is called once per frame
	void Update () {
		//reset position if enemy/player dies
		if(enemyObject.activeSelf && (enemyStateController.health <= 0 || enemyStateController.playerHealth.HPLeft <= 0))
		{
			enemyObject.transform.position = startPosition;
			//enemyObject.SetActive(false);
		}
	}
}
