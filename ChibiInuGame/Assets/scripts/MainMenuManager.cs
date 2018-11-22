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
		saveDatas[0] = SaveManager.Load("save1");
		saveDatas[1] = SaveManager.Load("save2");
		saveDatas[2] = SaveManager.Load("save3");
		//update the contents
		for(int index = 0; index < saveDatas.Length; ++index)
		{
			UpdateSaveSlotUI(saveSlotUI[index], saveDatas[index]);
		}
	}

	//load the level info and go to the level switch page
	public void LoadSaveFile(int index)
	{
		//no safe data, ask users enter name and create a new save file for them
		if(saveDatas[index] == null)
		{
			//get name
			//create new saveData
			SaveData newData = new SaveData("Test");
			//SaveManager.DebugJsonData(newData);
			SaveManager.dataInUse = newData;
			SaveManager.filename = "save" + (index + 1);
			SaveManager.Save(SaveManager.filename);
		}else//load the save file and update the levels
		{
			SaveManager.dataInUse = saveDatas[index];
			SaveManager.filename = "save" + (index + 1);
		}
		//go to levelSelectionScene
		UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
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
			slotUI.text = data.playerName;// + "\n" + (data.highestLevelAchieved/10) + "%";
		}
	}

}
