﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBoundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            //Instantiate(collision.gameObject.GetComponent<Projectile>().destroyParticle, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
