using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwitch : MonoBehaviour {

    public int newBackgroundArea;
    BackgroundSwitchManager backgroundSwitchManager;
	
    
    // Use this for initialization
	void Start () {
        backgroundSwitchManager = GetComponentInParent<BackgroundSwitchManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            backgroundSwitchManager.currentBackgroundArea = newBackgroundArea;
            backgroundSwitchManager.UpdateAciveBackground();
        }
    }


}
