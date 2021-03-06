﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour {

    public Transform target;
    public Rigidbody2D rb;
    public float speed;
    public float rotateSpeed;

    private float counter = 2;
    // Use this for initialization
    void Start()
    {
        DestroyObject(gameObject, 10);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.right).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DestroyObject(gameObject);
        }
        else if(collision.gameObject.tag == "AOE")
        {
            DestroyObject(gameObject, 2);
        }
    }
    
}
