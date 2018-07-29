using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtBox : MonoBehaviour {

    //public BoxCollider2D collider;
    public GameObject EnemyToKill;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.gameObject.transform.position.y - collision.gameObject.transform.position.y < 1)//Player above
        {
            Destroy(EnemyToKill);
        }
    }


}
