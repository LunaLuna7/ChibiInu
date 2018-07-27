using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAttack : MonoBehaviour {

    public Transform target;
    public float speed;
    Rigidbody2D rgbd;

    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();
       
        target = GameObject.FindGameObjectWithTag("Player").transform;

        transform.right = target.position - transform.position;
        rgbd.velocity = transform.right * speed;
      
    }


}
