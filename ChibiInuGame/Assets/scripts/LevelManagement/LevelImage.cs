using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelImage : MonoBehaviour {
	public GameObject collectablePage;
	public SpriteRenderer[] collectables;
	void Start()
	{
		//hide the collectables
		collectablePage.SetActive(false);
	}

	public void UpdateCollectableSprites(bool[] collected)
	{
		collectablePage.SetActive(true);
		//change color of collectables to show if have the collect
		for(int index = 0; index < collectables.Length; ++index)
		{
			//if get this collectable
			if(collected[index])
				collectables[index].color = Color.white;
		}
	}
}
