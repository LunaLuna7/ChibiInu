using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {


    public float jumpPower;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            rb.velocity = new Vector2(0, jumpPower);
            pc.JumpadOn();
        }
    }
}
