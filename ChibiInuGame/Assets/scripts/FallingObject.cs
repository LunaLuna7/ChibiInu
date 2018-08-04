using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {

    public GameObject parentObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "FallingObjectBoundary")
            Destroy(parentObj, .1f); //.1f to make sure the hitbox trigger is activated
    }
}
