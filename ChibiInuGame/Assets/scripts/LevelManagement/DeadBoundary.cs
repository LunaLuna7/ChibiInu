using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBoundary : MonoBehaviour {

    public Transform player;
    public GameManager gameManager;


    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            gameManager.GameOver(collision.gameObject.transform);
            
        }
    }

   
}
