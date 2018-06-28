using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCollectible : MonoBehaviour {

    private Boss boss;
    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        DestroyObject(gameObject, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DestroyObject(gameObject);
        }
    }
}
