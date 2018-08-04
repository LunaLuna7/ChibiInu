using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    //private Boss boss;
    public int lifeSpawn;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifeSpawn);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            //boss.HitBoss();
            //DestroyObject(gameObject);
        }
    }
}
