using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public LevelChanger levelChanger;

    // Use this for initialization
    void Awake () {
        levelChanger = GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GameOver()
    {
        levelChanger.FadeToSameLevel();
    }
}
