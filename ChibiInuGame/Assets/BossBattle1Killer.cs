using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle1Killer : MonoBehaviour {

    private SoundEffectManager soundEffectManager;


    void Start () {
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }
	
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            soundEffectManager.Stop("Boss");
            collision.gameObject.SetActive(false);
        }
    }
}
