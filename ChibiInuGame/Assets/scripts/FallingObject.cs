using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {

    public GameObject parentObj;
    private Transform originPosition;
    private Rigidbody2D rb;
    private void Awake()
    {
        originPosition = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Boundary")
        {
            
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000f, 0));
        }
            //Destroy(parentObj, .1f); //.1f to make sure the hitbox trigger is activated
    }

   
}
