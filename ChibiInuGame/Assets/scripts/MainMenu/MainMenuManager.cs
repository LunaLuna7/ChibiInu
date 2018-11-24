using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
	public GameObject chooseFilePage;
	public GameObject arrow;
	public GameObject[] buttons;
	
	// Use this for initialization
	void Start () {
		chooseFilePage.SetActive(false);
		//UpdateArrow(0);
	}
	
	// Update is called once per frame
	void Update () {
		if(!chooseFilePage.activeSelf)
			CheckMainInput();
	}

	//========================================================
	//deal with inputs
	//========================================================
	private int mainArrowIndex = 0;
	
	public void CheckMainInput()
	{
		//when press up button
		if(Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0 && mainArrowIndex >0)
		{
			--mainArrowIndex;
			UpdateArrow(mainArrowIndex);
		}
		//when press down
		else if(Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0 && mainArrowIndex < buttons.Length - 1)
		{
			++mainArrowIndex;
			UpdateArrow(mainArrowIndex);
		}
		//when press Space
		else if(Input.GetButtonDown("Jump"))
		{
			switch(mainArrowIndex)
			{
				//start game
				case 0:
					StartGameButton();
					break;
				case 1:
					break;
				//quit game
				case 2:
					QuitButton();
					break;
			}
		}
	}

	private void UpdateArrow(int index)
	{
		arrow.GetComponent<RectTransform>().position = new Vector3(arrow.GetComponent<RectTransform>().position.x, 
															  buttons[index].GetComponent<RectTransform>().position.y, 
															  arrow.GetComponent<RectTransform>().position.z);
	}

	//========================================================
	//Buttons
	//========================================================
	//let player choose from one of the three saveData slot
	public void StartGameButton()
	{
		chooseFilePage.SetActive(true);
		chooseFilePage.GetComponent<ChooseFilePage>().InitilizeSavePage();
	}

	//load the level info and go to the level switch page

	public void QuitButton()
	{
		Application.Quit();
		Debug.Log("Quit Game");
	}


}
