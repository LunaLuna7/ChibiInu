using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour {

    public List<Bird> birds;
    public List<Transform> birdAreas;

	// Use this for initialization
	void Start () {
        LocateBirds();
		for(int i = 0; i < this.transform.childCount; i++)
        {
            int temp = Random.Range(0, 2);
            transform.GetChild(i).GetComponent<SpriteRenderer>().flipX = (0 == temp);
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

    void LocateBirds()
    {
        int area = Random.Range(0, birdAreas.Count);
        transform.position = birdAreas[area].transform.position;

    }
}
