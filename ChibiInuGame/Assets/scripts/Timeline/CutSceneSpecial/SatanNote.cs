using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SatanNote : MonoBehaviour {
	public PlayableDirector timeline;
	public bool canContinue = false;
	
	void Awake()
	{
		timeline.Pause();
		StartCoroutine(SetCanClose());
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Submit") && canContinue)
		{
			timeline.Resume();
			gameObject.SetActive(false);
		}
	}

	//player need to read the letter for at least one second
	IEnumerator SetCanClose()
	{
		yield return new WaitForSeconds(1);
		canContinue = true;
	}
}
