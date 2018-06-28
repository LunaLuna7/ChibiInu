using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    private Snake next;

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

    
}
