using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour {
	public GameObject chosenKey;
	[SerializeField]private GameObject[] keys;
	public Color selectedColor;
	public Color unSelectedColor;
	private bool caps = false;
	private int currentIndex = 0;
	

	public void Reset()
	{
		if(chosenKey != null)
			chosenKey.GetComponent<Image>().color = unSelectedColor;
		chosenKey = keys[0];
		SelectKey(0);
		currentIndex = 0;
		//default caps is false
		caps = true;
		ChangeCapitalization();
	}

	//==========================================================================================================================
	//Navigate the board
	//==========================================================================================================================
	public void MoveLeft()
	{
		//do nothing if at q,a,z
		if(currentIndex == 0 || currentIndex == 11 || currentIndex == 22)
			return;
		//at enter button
		if(currentIndex == 20 || currentIndex == 21)
			currentIndex = 19;
		//at caps button
		else if(currentIndex == 31 || currentIndex == 32)
			currentIndex = 30;
		else
			currentIndex -= 1;
		SelectKey(currentIndex);
	}

	public void MoveRight()
	{
		//do nothing if at delete, enter, caps
		if(currentIndex == 10 || currentIndex == 20 || currentIndex == 21 || currentIndex == 31 || currentIndex == 32)
			return;
		else
			currentIndex += 1;
		SelectKey(currentIndex);
	}

	public void MoveUp()
	{
		//do nothing if at top row
		if(currentIndex >= 0 && currentIndex <= 10)
			return;
		else if(currentIndex == 20)
			currentIndex = 10;
		else
			currentIndex -= 11;
		SelectKey(currentIndex);
	}

	public void MoveDown()
	{
		//do nothing if at bottom row
		if(currentIndex >= 22 && currentIndex <= 32)
			return;
		else
			currentIndex += 11;
		SelectKey(currentIndex);
	}

	private void SelectKey(int keyIndex)
	{
		//change color to show the selected key
		chosenKey.GetComponent<Image>().color = unSelectedColor;
		chosenKey = keys[keyIndex];
		chosenKey.GetComponent<Image>().color = selectedColor;
	}
	//==========================================================================================================================
	//Interact with ChangeNewFilePage
	//==========================================================================================================================
	public string GetKeyValue()
	{
		return GetTextCompoent(chosenKey).text;
	}

	//return Text Component of a key object
	private Text GetTextCompoent(GameObject key)
	{
		return key.transform.Find("Text").GetComponent<Text>();
	}
	public void ChangeCapitalization()
	{
		//change to lower case
		if(caps)
		{
			caps = false;
			for(int x = 0; x<=30; ++x)
			{
				//do nothing for delete and enter key
				if(x == 10 || x == 20 || x == 21)
					continue;
				//:/;
				else if(x == 29)
					GetTextCompoent(keys[x]).text = ":";
				//!/?
				else if(x == 30)
					GetTextCompoent(keys[x]).text = "!";
				else
					GetTextCompoent(keys[x]).text = GetTextCompoent(keys[x]).text.ToLower();
			}
		}else//chaneg to upper case
		{
			caps = true;
			for(int x = 0; x<=30; ++x)
			{
				//do nothing for delete and enter key
				if(x == 10 || x == 20 || x == 21)
					continue;
				//:/;
				else if(x == 29)
					GetTextCompoent(keys[x]).text = ";";
				//!/?
				else if(x == 30)
					GetTextCompoent(keys[x]).text = "?";
				else
					GetTextCompoent(keys[x]).text = GetTextCompoent(keys[x]).text.ToUpper();
			}
		}
	}

}
