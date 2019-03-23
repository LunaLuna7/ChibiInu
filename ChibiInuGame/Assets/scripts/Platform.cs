using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private float timer;
    private GameObject player;
    public bool touchedPlayer;

    [Header("Float Stats")]
    public bool floaty;
    public float offset;
    public float speed;
    bool acending;
    float originalHeight;

    private void Start()
    {
        originalHeight = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player");
        touchedPlayer = false;
        acending = true;
        //if(floaty)
           // StartCoroutine(Float());
    }

    private void Update()
    {

        if (!player.GetComponent<CharacterController2D>().IsGrounded())
            player.transform.SetParent(null);

        Float();
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

    private void OnDisable()
    {
        player.transform.SetParent(null);
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

    void Float()
    {
        if (acending)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, offset, 0), speed * Time.deltaTime);
            if (transform.position.y >= originalHeight + offset)
                acending = false;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0, offset, 0), speed * Time.deltaTime);
            if (transform.position.y < originalHeight)
                acending = true;
        }
        
    }
}
