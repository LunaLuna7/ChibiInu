using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("a");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("dead");
            DestroyObject(collision.gameObject);
        }
    }
}
