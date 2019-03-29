using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle1Killer : MonoBehaviour {

    private SoundEffectManager soundEffectManager;
    public GameObject Boss;
    public GameObject BossDeadParticle;

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
            Instantiate(BossDeadParticle, transform.position - new Vector3(20,20,0), Quaternion.identity);
        }
    }
}
