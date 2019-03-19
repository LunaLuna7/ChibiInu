using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour {

    public List<Bird> birds;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < this.transform.childCount; i++)
        {
            birds.Add(transform.GetChild(i).GetComponent<Bird>());
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            for(int i = 0; i < birds.Count; i++)
            {
                birds[i].triggerFly = true;
            }
        }
    }
}
