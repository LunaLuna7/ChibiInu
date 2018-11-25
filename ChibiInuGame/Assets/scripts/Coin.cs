using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public int collectableIndex;//which one of 3 collectable coins is this one
    public LevelEnd levelEnd;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            levelEnd.Collect(collectableIndex);
            Destroy(this.gameObject);
        }
    }
}
