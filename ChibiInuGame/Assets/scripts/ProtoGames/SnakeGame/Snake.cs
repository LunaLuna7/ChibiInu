using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    private Snake next;
    static public System.Action<System.String> hit;

    public void SetNext(Snake snake)
    {
        next = snake;
    }

    public Snake GetNext()
    {
        return next;
    }

    public void RemoveTail()
    {
        DestroyObject(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hit != null)
        {
            hit(collision.tag);
        }

        if(collision.tag == "food")
        {
            Destroy(collision.gameObject);
        }

        
    }

}
