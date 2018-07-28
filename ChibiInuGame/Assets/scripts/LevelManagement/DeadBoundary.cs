using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBoundary : MonoBehaviour {

    private LevelChanger levelChanger;
	// Use this for initialization
	void Start () {
        levelChanger = GetComponentInParent<LevelChanger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            levelChanger.FadeToSameLevel();
        }
    }
}
