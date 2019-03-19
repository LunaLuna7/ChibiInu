using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwitchManager : MonoBehaviour {

    public List<GameObject> backgrounds;
    public int currentBackgroundArea;

	// Use this for initialization
	void Start () {
        currentBackgroundArea = 0;
        UpdateAciveBackground();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateAciveBackground()
    {
        for(int i = 0; i < backgrounds.Count; i++)
        {
            
            if (i == currentBackgroundArea)
                backgrounds[i].SetActive(true);
            else
                backgrounds[i].SetActive(false);
        }
    }
}
