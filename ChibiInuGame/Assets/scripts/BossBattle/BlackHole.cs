using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DestroyObject(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack" || collision.gameObject.tag == "AOE")
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, transform.position, .05f);
        }
    }

    /*
     * accidental repeal property
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "attack")
        {
            Transform otherTransform = collision.GetComponent<Transform>();
            Vector2 direction = otherTransform.position - transform.position;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(100 * direction);
        }
    }
    */
}
