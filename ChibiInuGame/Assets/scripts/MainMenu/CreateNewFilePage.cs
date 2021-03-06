﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewFilePage : MonoBehaviour {
	[Header("Functional parameters")]
	public int maxCharacterAllowed;
	[Header("Objects needed")]
	public ChooseFilePage chooseFilePage;
	public Text inputFieldText;
	public GameObject confirmWindow;
	public Text confirmText;
	private string newName;
	private int newSlotIndex;
	public GameObject virtualKeyboardObject;
	public GameObject promptForKeyboard;
	public VirtualKeyboard virtualKeyboard;
	public Text[] nameCharacterUIs;
	public ImageColorfulEffect enterImageEffect;

	void OnEnable()
	{
		MenuInputManager.SetButtonName("ControllerHori", "ControllerVerti");
	}

	void OnDisable()
	{
		MenuInputManager.SetButtonName("Horizontal", "MainMenuVertical");
	}
	// Use this for initialization
	void Start () {
		newName = "";
		UpdateInputText("");
		confirmWindow.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!confirmWindow.activeSelf)
		{
			//checking if controller is connected, and switch mode due to it
			Debug.Log(Input.GetJoystickNames()[0].ToString());
			if(ControllerConnected())
			{
				//show virtual keyboard, hide prompt
				if(!virtualKeyboardObject.activeSelf) virtualKeyboardObject.SetActive(true);
				if(promptForKeyboard.activeSelf) promptForKeyboard.SetActive(false);
				CheckVirtualKeyboardInput();
			}
			else
			{
				//hide virtual keyboard, show prompt
				if(virtualKeyboardObject.activeSelf) virtualKeyboardObject.SetActive(false);
				if(!promptForKeyboard.activeSelf) promptForKeyboard.SetActive(true);
				//for keyboard
				CheckModifyName();
			}
			CheckFinishAndCancel();
		}
		else//confirm if 
		{
			CheckConfirm();
		}
	}

	private bool ControllerConnected()
	{
		string[] controllerList = Input.GetJoystickNames();
		if(controllerList.Length > 0)
		{
			foreach(string controllerName in controllerList)
			{
				//if name is empty, still not a controller or it is disabled
				if(controllerName.Length > 0)
					return true;
			}
		}
		return false;
	}

	public void Initalize(int targetIndex)
	{
		gameObject.SetActive(true);
		newName = "";
		UpdateInputText("");
		confirmWindow.SetActive(false);
		newSlotIndex = targetIndex;
		virtualKeyboard.Reset();
	}

	//==========================================================================================================================
	//Check input
	//==========================================================================================================================
	public void CheckModifyName()
	{
		if(Input.GetKeyDown(KeyCode.Backspace))
		{
			DeleteCharacter();
		}//delete character
		else if(Input.GetKeyDown(KeyCode.Return))
		{
			FinishInput();
		}//users type new name
		else if(Input.anyKeyDown)
		{
			string character = Input.inputString;
			//make sure it is a character
			if(character.GetHashCode() <= 122 && character.GetHashCode() >= 65)
			{
				AddCharacter(character);
			}
		}
	}

	public void CheckVirtualKeyboardInput()
	{
		//for controller
		//navigate the board
		if(MenuInputManager.CheckLeft())
			virtualKeyboard.MoveLeft();
		else if(MenuInputManager.CheckRight())
			virtualKeyboard.MoveRight();
		else if(MenuInputManager.CheckUp())
			virtualKeyboard.MoveUp();
		else if(MenuInputManager.CheckDown())
			virtualKeyboard.MoveDown();
		//comfirm/type
		else if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
		{
			string key = virtualKeyboard.GetKeyValue();
			Debug.Log(key);
			if(key == "caps")
			{
				virtualKeyboard.ChangeCapitalization();
			}else if(key == "enter")
			{
				FinishInput();
			}else if(key == "delete"){
				DeleteCharacter();
			}else{
				AddCharacter(key);
			}
		}
	}
	//==========================================================================================================================
	//Process
	//==========================================================================================================================
	public void CheckFinishAndCancel()
	{
		/*confirm typing
		if(Input.GetButtonDown("Submit"))
		{
			FinishInput();
		}*/
		//cancel creating new file
		if(Input.GetButtonDown("Cancel"))
		{
			gameObject.SetActive(false);
		}
	}

	public void CheckConfirm()
	{
		//confirm to use this name
		if(Input.GetButtonDown("Submit"))
		{
			chooseFilePage.CreateNewFile(newSlotIndex, newName);
			//close create new file Panel
			gameObject.SetActive(false);
		}
		//return back to modify name
		if(Input.GetButtonDown("Cancel"))
		{
			confirmWindow.SetActive(false);
		}
	}

	//add a new character to the end of name
	private void AddCharacter(string character)
	{
		if(newName.Length < maxCharacterAllowed)
		{
			newName += character;
			UpdateInputText(newName);
			//enter's image effect start when name gets to 1 from 0
			if(newName.Length == 1)
				enterImageEffect.StartEffect();
		}
	}
	//remove the last character in the name
	private void DeleteCharacter()
	{
		if(newName.Length > 0)
		{
			newName = newName.Substring(0, newName.Length - 1);
			UpdateInputText(newName);
			//enter's image effect stops when name is empty
			if(newName.Length <= 0)
				enterImageEffect.StopEffect();
		}
	}
	//finish input and open comfirm page
	private void FinishInput()
	{
		if(newName != "")
		{
			//hide prompt for keyboard first
			if(promptForKeyboard.activeSelf) promptForKeyboard.SetActive(false);
			confirmWindow.SetActive(true);
			confirmText.text = "Your name is " + newName +" ?";
		}
	}
	//update UI to show current name, need to add space between characters to make sure texts are on the underline
	private void UpdateInputText(string value)
	{
		inputFieldText.text = "";
		int len = value.Length;
		//display the name
		for(int x = 0; x< len; ++x)
			nameCharacterUIs[x].text = value[x] + "";
		//clean empty texts
		for(int x = len; x< maxCharacterAllowed; ++x)
			nameCharacterUIs[x].text = "";
	}

}
