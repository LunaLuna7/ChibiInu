using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private float timer;
    private GameObject player;
    public bool touchedPlayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        touchedPlayer = false;
    }

    private void Update()
    {

        if (!player.GetComponent<CharacterController2D>().IsGrounded())
            player.transform.SetParent(null);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchedPlayer = true;
            collision.transform.SetParent(transform);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    /*
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (timer > 1)
                collision.collider.transform.SetParent(null);
            else
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
            }
        }
    }*/
}
