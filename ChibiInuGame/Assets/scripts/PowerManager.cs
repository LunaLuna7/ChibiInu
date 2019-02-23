using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour {

    public CastlePlatform castlePlatform;
    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToDisappear;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEnemies()
    {
        foreach(GameObject enemies in objectsToAppear)
        {
            enemies.SetActive(true);
        }
        foreach (GameObject each in objectsToDisappear)
            each.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SpawnEnemies();
    }
}
