using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.P))
        {
            gameObject.SetActive(true);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("FireBall"))
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }      
    }
}

