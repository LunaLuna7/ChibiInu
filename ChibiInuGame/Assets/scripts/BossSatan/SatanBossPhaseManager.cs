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
		{
			currentMap.SetActive(false);
			player.transform.position = playerStartPositions[index].position;
		}
		//set current map
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
		GoToPhase(phase);
	}

    //Returns current map we are on. Phase 0 =>returns 0, Phase 1 =? returns 1 and so on...
    public int GetPhaseMap()
    {
        int counter = -1;
        foreach(GameObject each in maps)
        {
            counter++;
            if (each == currentMap)
                return counter;
                
        }
        if (counter == -1)
            Debug.LogError("Not currently on a existing map level piece for satan");
        return counter;
    }

}
