using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPorjectile : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
    public float lifeSpam = 3f;
    private Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeSpam);
    }

    // Update is called once per frame
    void Update()
    {
        if(speed < 0)
            transform.Rotate(0, 0, 1 * rotationSpeed);
        else
            transform.Rotate(0,0,-1 * rotationSpeed);
        
    }
}
