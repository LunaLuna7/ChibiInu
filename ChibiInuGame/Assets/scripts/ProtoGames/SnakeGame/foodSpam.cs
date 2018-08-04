using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodSpam : MonoBehaviour {

    public GameObject food;
    public Vector2 boundary;
    public float startSpawnTime;
    public float spawnTime;

	// Use this for initialization
	void Start () {
        InvokeRepeating("CreateFood", startSpawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateFood()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-boundary.x, boundary.x), Random.Range(-boundary.y, boundary.y));
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(food, spawnPosition, spawnRotation);
    }
}
