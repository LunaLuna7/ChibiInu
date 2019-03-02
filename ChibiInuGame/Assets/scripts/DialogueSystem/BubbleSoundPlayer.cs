using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSoundPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PlaySound(string soundName)
	{
		SoundEffectManager.instance.Play(soundName);
	}
}
