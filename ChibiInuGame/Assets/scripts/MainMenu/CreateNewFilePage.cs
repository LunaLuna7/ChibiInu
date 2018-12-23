using System.Collections;
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
	public VirtualKeyboard virtualKeyboard;

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
			//CheckModifyName();
			CheckVirtualKeyboardInput();
			CheckFinishAndCancel();
		}
		else//confirm if 
		{
			CheckConfirm();
		}
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
		//users type new name
		if(Input.anyKeyDown)
		{
			string character = Input.inputString;
			//make sure it is a character
			if(character.GetHashCode() <= 122 && character.GetHashCode() >= 65)
			{
				AddCharacter(character);
			}
		}
		//users delete character
		if(Input.GetKeyDown(KeyCode.Backspace))
		{
			DeleteCharacter();
		}
	}

	public void CheckVirtualKeyboardInput()
	{
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
		else if(Input.GetButtonDown("Submit"))
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
		}
	}
	//remove the last character in the name
	private void DeleteCharacter()
	{
		if(newName.Length > 0)
		{
			newName = newName.Substring(0, newName.Length - 1);
			UpdateInputText(newName);
		}
	}
	//finish input and open comfirm page
	private void FinishInput()
	{
		if(newName != "")
		{
			confirmWindow.SetActive(true);
			confirmText.text = "Your name is " + newName +" ?";
		}
	}
	private void UpdateInputText(string value)
	{
		inputFieldText.text = value;
	}

}
