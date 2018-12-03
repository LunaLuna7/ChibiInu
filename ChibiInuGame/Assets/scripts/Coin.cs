using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Coin : MonoBehaviour {

    public int collectableIndex;//which one of 3 collectable coins is this one
    public LevelEnd levelEnd;
    public Image coinUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            coinUI.color = new Color(coinUI.color.r, coinUI.color.g, coinUI.color.b, 1);
            levelEnd.Collect(collectableIndex);
            Destroy(this.gameObject);
        }
    }
}
