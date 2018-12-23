using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFilePage : MonoBehaviour {
	public GameObject arrow;
	public GameObject[] buttons;
	public Text[] saveSlotUI = new Text[3];
	private SaveData[] saveDatas = new SaveData[3];
	public GameObject createNewFilePage;
	public LevelChanger levelChanger;
    public SoundEffectManager soundEffectManager;
	private bool canMove = true;

    private void Start()
    {
        soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffect").GetComponent<SoundEffectManager>();

    }

    void Update () {
		if(!createNewFilePage.activeSelf && canMove)
			CheckInput();
	}

	//========================================================
	//Choosing file/ Change index
	//========================================================
	private int arrowIndex = 0;
	public void CheckInput()
	{
		//when press up button
		if(MenuInputManager.CheckUp() && arrowIndex >0)
		{
            soundEffectManager.Play("MenuScroll");

            UpdateArrow(--arrowIndex);
		}
		//when press down
		else if(MenuInputManager.CheckDown() && arrowIndex < buttons.Length - 1)
		{
            soundEffectManager.Play("MenuScroll");

            UpdateArrow(++arrowIndex);
		}
		//when press Space
		else if (Input.GetButtonDown("Submit"))
        {
            soundEffectManager.Play("MenuSelect");
			canMove = false;
            LoadSaveFile(arrowIndex);
        }
		else if(Input.GetButtonDown("Cancel") || Input.GetMouseButtonDown(1)) {
            soundEffectManager.Play("MenuSelect");
            ReturnFromChooseFile();
        }
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
			createNewFilePage.GetComponent<CreateNewFilePage>().Initalize(index);
		}else//load the save file and update the levels
		{
			SaveManager.dataInUse = saveDatas[index];
			SaveManager.filename = "save" + (index + 1);
			//go to levelSelectionScene
			levelChanger.FadeToLevel(1);
		}
	}

	public void CreateNewFile(int slotIndex, string playerName)
	{
		//create new saveData
		SaveData newData = new SaveData(playerName);
		SaveManager.dataInUse = newData;
		SaveManager.filename = "save" + (slotIndex + 1);
		SaveManager.Save(SaveManager.filename);
		saveDatas[slotIndex] = newData;
		UpdateSaveSlotUI(saveSlotUI[slotIndex], newData);
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
