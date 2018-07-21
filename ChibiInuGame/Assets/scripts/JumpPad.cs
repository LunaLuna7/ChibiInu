using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    public float angle;
    public float power;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            //rb.AddForce(Vector3.up * 300 * power);

            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            rb.AddForce(dir * 100 * power);
        }
    }
}
