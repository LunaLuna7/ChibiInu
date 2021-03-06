﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSoundArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SoundEffectManager.instance.Play("LaserDrone");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundEffectManager.instance.Stop("LaserDrone");
            SoundEffectManager.instance.Play("LaserOff");
        }
    }
}
