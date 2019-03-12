using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossPhaseManager : MonoBehaviour {
	int phase = 0;
	//maps
	public GameObject[] maps;
	public Transform[] playerStartPositions;
	public GameObject player;
	private GameObject currentMap;


	public void GoToNextPhase()
	{
		++phase;
		GoToPhase(phase);
	}

	private void GoToPhase(int index)
	{
		//hide last map
		if(currentMap)
			currentMap.SetActive(false);
		//set current map
		currentMap = maps[index];
		currentMap.SetActive(true);
		player.transform.position = playerStartPositions[index].position;
	}

	public void Reset()
	{
		phase = 0;
		currentMap = null;
		//hide maps for phase 1-3
		for(int x = 1; x < maps.Length; ++x)
			maps[x].SetActive(false);
		GoToPhase(phase);
	}

}
