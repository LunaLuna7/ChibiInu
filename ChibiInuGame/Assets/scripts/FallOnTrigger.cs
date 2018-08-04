using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallOnTrigger : MonoBehaviour {

    public GameObject fallingObject;
    private Rigidbody2D m_rb;
    private void Start()
    {
        m_rb = fallingObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_rb.isKinematic = false;
        }
    }
}
