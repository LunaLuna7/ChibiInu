using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPartner : MonoBehaviour {

    public Partner partner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            partner.unlocked = true;
            //play animation
        }
    }
}
