using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public int health;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "attack")
        {
            DestroyObject(collision.gameObject);
            health--;
        }
        if(health <= 0)
        {
            DestroyObject(gameObject);
        }
    }


}
