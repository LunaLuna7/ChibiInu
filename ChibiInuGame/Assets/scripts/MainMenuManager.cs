using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
	public GameObject chooseFilePage;
	public Text[] saveSlotUI = new Text[3];
	private SaveData[] saveDatas = new SaveData[3];
	// Use this for initialization
	void Start () {
		chooseFilePage.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//========================================================
	//Buttons
	//========================================================
	
	//let player choose from one of the three saveData slot
	public void StartGameButton()
	{
		chooseFilePage.SetActive(true);
		//load files
		saveDatas[0] = SaveManager.Load("Save1");
		saveDatas[1] = SaveManager.Load("Save2");
		saveDatas[2] = SaveManager.Load("Save3");
		//update the contents
		for(int index = 0; index < saveDatas.Length; ++index)
		{
			UpdateSaveSlotUI(saveSlotUI[index], saveDatas[index]);
		}
	}

	//load the level info and go to the level switch page
	public void LoadSaveFile(int index)
	{
		
	}

	public void ReturnFromChooseFileButton()
	{
		chooseFilePage.SetActive(false);
	}

	public void QuitButton()
	{
		Application.Quit();
	}
	//========================================================
	//Modifing UI contents
	//========================================================
	public void UpdateSaveSlotUI(Text slotUI, SaveData data)
	{
		if(data == null){
			slotUI.text = "Start a New Game";
		}
		else{
			slotUI.text = data.playerName + "\n" + (data.highestLevelAchieved/10);
		}
	}

}
