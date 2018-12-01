using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualKeyboard : MonoBehaviour {
	public GameObject choiceBox;
	private Dictionary<int, string> keyDict = new Dictionary<int, string>{
		{0, "q"}, {1, "w"}, {2, "e"}, {3, "r"}, {4, "t"}, {5, "y"}, {6, "u"}, {7, "i"}, {8, "o"}, {9, "p"}, {10, "delete"},
		{11, "a"}, {12, "s"}, {13, "d"}, {14, "f"}, {15, "g"}, {16, "h"}, {17, "j"}, {18, "k"}, {19, "l"}, {20, "enter"}, {21, "enter"},
		{22, "z"}, {23, "x"}, {24, "c"}, {25, "v"}, {26, "b"}, {27, "n"}, {28, "m"}, {29, ":"}, {30, "!"}, {31, "caps"}, {32, "caps"},
	};
	private bool caps = false;
	private int currentIndex = 0;
	
	public void Reset()
	{
		currentIndex = 0;
		caps = false;
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
	}

	public void MoveRight()
	{
		//do nothing if at delete, enter, caps
		if(currentIndex == 10 || currentIndex == 20 || currentIndex == 21 || currentIndex == 31 || currentIndex == 32)
			return;
		else
			currentIndex += 1;
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
	}

	public void MoveDown()
	{
		//do nothing if at bottom row
		if(currentIndex >= 22 && currentIndex <= 32)
			return;
		else
			currentIndex += 11;
	}

	public string GetCharacter()
	{
		return keyDict[currentIndex];
	}

	private void UpdateChoiceBox(int currentIndex)
	{
		
	}
}
