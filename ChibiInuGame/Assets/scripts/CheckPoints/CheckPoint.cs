using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public int newCheckPoint;

    public void SetCheckPointTo()
    {
        UpdateCheckPoint.currentCheckPoint = newCheckPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SetCheckPointTo();
        }
    }

}
