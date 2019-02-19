﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlePlatform : MonoBehaviour {

    public float speed;
    public bool active;
    public Transform destination;
    private Rigidbody2D rb;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(active)
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
	}
}