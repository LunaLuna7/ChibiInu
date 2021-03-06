﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour {
	private bool[] collectable;
	public LevelChanger levelChanger;
	public PartnerManager partnerManager;
	[SerializeField]private int levelIndex;
	[SerializeField]private int nextLevelIndex;
    public static bool levelEnding;

	[Header("Level End UI")]
	public GameObject levelEndUI;
	public Image[] coinArray;
	public Image[] macmaffinArray;
	public Color notCollectedColor;

    private void Awake()
    {
        levelEnding = false;
    }

    // Use this for initialization
    void Start () {
		//collectable = SaveManager.dataInUse.levels[levelIndex].collectable;
		collectable = new bool[3]{false, false, false};
		//if forget to drag PM here, let's get it through code
		if(partnerManager == null)
			partnerManager = GameObject.FindObjectOfType<PartnerManager>();
		levelEndUI.SetActive(false);
	}

	void Update()
	{
		if(levelEndUI.activeSelf && Input.GetButtonDown("Submit"))
		{
			BackToMap();
		}
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		//touch the end, save and transfer back to level selection
		if(other.CompareTag("Player"))
		{
            SoundEffectManager.instance.Play("LevelCompleted");
			ShowEndUI();
		}
	}

	public void ShowEndUI()
	{
		//unlock next level, update the stuff collected, save
		SaveManager.dataInUse.levels[nextLevelIndex].unlocked = true;
		//update collectable
		//SaveManager.dataInUse.levels[levelIndex].collectable = collectable;
		for(int x = 0; x < collectable.Length; ++x)
		{
			if(collectable[x] == true)
				SaveManager.dataInUse.levels[levelIndex].collectable[x] = true;
		}
		SaveManager.dataInUse.lastLevelEntered = levelIndex;
		//save the partner info to the save Data
		if(partnerManager != null)
		{
			SaveManager.dataInUse.GetPartnerInfo(partnerManager);
		}
		SaveManager.Save(SaveManager.filename);
		//show UI
		levelEndUI.SetActive(true);
		//adjust coin color depends on if collect or not
		for(int x = 0; x < collectable.Length; ++x)
		{
			if(collectable[x])
			{
				coinArray[x].color = Color.white;
			}else{
				coinArray[x].color = notCollectedColor;
			}
		}
		//adjust coin color depends on the level unlocked
		for(int x = 0; x < macmaffinArray.Length; ++x)
			macmaffinArray[x].color = notCollectedColor;
		if(SaveManager.dataInUse.levels[3].unlocked)
			macmaffinArray[0].color = Color.white;
		if(SaveManager.dataInUse.levels[6].unlocked)
			macmaffinArray[1].color = Color.white;
		if(SaveManager.dataInUse.levels[9].unlocked)
			macmaffinArray[2].color = Color.white;
		//don't allow player to move
		CharacterController2D playerController = GameObject.FindObjectOfType<CharacterController2D>();
		if(playerController != null)
		{
			playerController.m_Paralyzed = true;
		}
	}

	public void BackToMap()
	{
        levelEnding = true;
		//transfer to LevelSeletion Scene
		levelChanger.FadeToLevel(1);
	}

	public void Collect(int index)
	{
		collectable[index] = true;
	}
}
