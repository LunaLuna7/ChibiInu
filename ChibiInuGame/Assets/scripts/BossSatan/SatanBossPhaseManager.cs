using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatanBossPhaseManager : MonoBehaviour {
	int phase = 0;
	//maps
	public GameObject[] maps;
	public Transform[] playerStartPositions;
	public GameObject player;
	private GameObject currentMap;
	public Image curtain;


	public void GoToNextPhase()
	{
		++phase;
		StartCoroutine(GoToPhase(phase, 0.3f));
	}

	private IEnumerator GoToPhase(int index, float time)
	{
		//turn screen to black
		Color currentColor = curtain.color;
		float interval = 1f/(30 * time);
		for(float x = 0; x < 1; x += interval)
		{
			currentColor.a = x;
			curtain.color = currentColor;
			yield return new WaitForEndOfFrame();
		}
		SetPhase(index);
		yield return new WaitForEndOfFrame();
		//show curtain
		for(float x = 1; x > 0; x -= interval)
		{
			currentColor.a = x;
			curtain.color = currentColor;
			yield return new WaitForEndOfFrame();
		}
	}

	private void SetPhase(int index)
	{
		//hide current map
		if(currentMap)
		{
			currentMap.SetActive(false);
			player.transform.position = playerStartPositions[index].position;
		}
		//set new map
		currentMap = maps[index];
		currentMap.SetActive(true);
	}

	public void Reset()
	{
		phase = 0;
		currentMap = null;
		//hide maps for phase 1-3
		for(int x = 1; x < maps.Length; ++x)
			maps[x].SetActive(false);
		SetPhase(phase);
	}

}
