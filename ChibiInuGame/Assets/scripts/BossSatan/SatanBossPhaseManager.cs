using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossPhaseManager : MonoBehaviour {
	int phase = 0;
	//maps
	public GameObject[] maps;
	private GameObject currentMap;


	public void GoToNextPhase()
	{
		++phase;
		GoToPhase(phase);
	}

	private void GoToPhase(int index)
	{
		currentMap = maps[index];
	}

	public void Reset()
	{
		phase = 0;
		//GoToPhase(phase);

	}

}
