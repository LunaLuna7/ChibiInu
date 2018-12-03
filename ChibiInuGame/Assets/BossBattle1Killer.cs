using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle1Killer : MonoBehaviour {

    private SoundEffectManager soundEffectManager;
    public GameObject Boss;


    void Start () {
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }
	
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GSlimeHitBox")
        {
            soundEffectManager.Stop("Boss");
            Boss.SetActive(false);
        }
    }
}
