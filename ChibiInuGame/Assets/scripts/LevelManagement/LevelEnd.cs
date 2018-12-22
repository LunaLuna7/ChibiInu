using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {
	private bool[] collectable;
	public LevelChanger levelChanger;
	public PartnerManager partnerManager;
	[SerializeField]private int levelIndex;
	[SerializeField]private int nextLevelIndex;
	// Use this for initialization
	void Start () {
		collectable = SaveManager.dataInUse.levels[levelIndex].collectable;
		//if forget to drag PM here, let's get it through code
		if(partnerManager == null)
			partnerManager = GameObject.FindObjectOfType<PartnerManager>();
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		//touch the end, save and transfer back to level selection
		if(other.CompareTag("Player"))
		{
			//unlock next level, update the stuff collected, save
			SaveManager.dataInUse.levels[nextLevelIndex].unlocked = true;
			SaveManager.dataInUse.levels[levelIndex].collectable = collectable;
			SaveManager.dataInUse.lastLevelEntered = levelIndex;
			//save the partner info to the save Data
			if(partnerManager != null)
			{
				SaveManager.dataInUse.GetPartnerInfo(partnerManager);
			}
			SaveManager.Save(SaveManager.filename);
			//transfer to LevelSeletion Scene
			levelChanger.FadeToLevel(1);
		}
	}

	public void Collect(int index)
	{
		collectable[index] = true;
	}
}
