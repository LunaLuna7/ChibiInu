using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFilePage : MonoBehaviour {
	public GameObject arrow;
	public GameObject[] buttons;
	public Text[] saveSlotUI = new Text[3];
	private SaveData[] saveDatas = new SaveData[3];
	void Update () {
		CheckInput();
	}

	//========================================================
	//Choosing file/ Change index
	//========================================================
	private int arrowIndex = 0;
	public void CheckInput()
	{
		//when press up button
		if(Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0 && arrowIndex >0)
		{
			UpdateArrow(--arrowIndex);
		}
		//when press down
		else if(Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0 && arrowIndex < buttons.Length - 1)
		{
			UpdateArrow(++arrowIndex);
		}
		//when press Space
		else if(Input.GetButtonDown("Jump"))
			LoadSaveFile(arrowIndex);
		else if(Input.GetButtonDown("Cancel") || Input.GetMouseButtonDown(2))
			ReturnFromChooseFile();
	}

	private void UpdateArrow(int index)
	{
		arrowIndex = index;
		arrow.GetComponent<RectTransform>().position = new Vector3(arrow.GetComponent<RectTransform>().position.x, 
															  buttons[index].GetComponent<RectTransform>().position.y, 
															  arrow.GetComponent<RectTransform>().position.z);
	}
	public void ReturnFromChooseFile()
	{
		Debug.Log(1);
		gameObject.SetActive(false);
	}

	//========================================================
	//Save Contents
	//========================================================
	public void InitilizeSavePage()
	{
		//load files
		saveDatas[0] = SaveManager.Load("save1");
		saveDatas[1] = SaveManager.Load("save2");
		saveDatas[2] = SaveManager.Load("save3");
		//update the contents
		for(int index = 0; index < saveDatas.Length; ++index)
		{
			UpdateSaveSlotUI(saveSlotUI[index], saveDatas[index]);
		}
		UpdateArrow(0);
	}

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
