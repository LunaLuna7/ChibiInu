using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatanBossPhaseManager : MonoBehaviour {
	int phase = 0;
	//maps
	public GameObject[] maps;
	public Transform[] playerStartPositions;
	public PartnerManager partnerManager;
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
		//add partner depends on map
		if(phase == 1)//wizard
			partnerManager.SummonPartner(SkillSlot.FirstSlot, partnerManager.partners[0]);
		if(phase == 2)//bard
			partnerManager.SummonPartner(SkillSlot.SecondSlot, partnerManager.partners[1]);
	}

	public void Reset()
	{
		phase = 0;
		currentMap = null;
		//hide maps for phase 1-3
		for(int x = 1; x < maps.Length; ++x)
			maps[x].SetActive(false);
		SetPhase(phase);
		partnerManager.UnsummonAllPartners();
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
