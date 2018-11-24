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
			CheckModifyName();
			CheckFinish();
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
	}
	public void CheckModifyName()
	{
		//users type new name
		if(newName.Length < maxCharacterAllowed && Input.anyKeyDown)
		{
			string character = Input.inputString;
			//make sure it is a character
			if(character.GetHashCode() <= 122 && character.GetHashCode() >= 65)
			{
				newName += character;
				UpdateInputText(newName);
			}
		}
		//users delete character
		if(newName.Length > 0 && Input.GetKeyDown(KeyCode.Backspace))
		{
			newName = newName.Substring(0, newName.Length - 1);
			UpdateInputText(newName);
		}
	}

	public void CheckFinish()
	{
		//confirm typing
		if(Input.GetButtonDown("Submit") && newName != "")
		{
			confirmWindow.SetActive(true);
			confirmText.text = "Your name is " + newName +" ?";
		}
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

	private void UpdateInputText(string value)
	{
		inputFieldText.text = value;
	}

}
