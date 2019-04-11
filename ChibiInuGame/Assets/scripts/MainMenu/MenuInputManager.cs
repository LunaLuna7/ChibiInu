using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputManager : MonoBehaviour {
	enum ArrowKey{Left, Right,Up,Down,None}

	private static string horizontalButtonName = "Horizontal";
	private static string verticalButtonName = "Vertical";
	//information I need
	private static ArrowKey lastVerticalArrow = ArrowKey.None;
	private static ArrowKey lastHorizontalArrow = ArrowKey.None;
	private static float timer = 0;
	private const float timeInterval = 0.2f; 
	
	// Update is called once per frame
	void Update () {
		if(timer < timeInterval)
			timer += Time.deltaTime;
		//if unpress a button, allow players to press it again
		if(lastVerticalArrow == ArrowKey.Up && Input.GetAxis(verticalButtonName) < 0.1f)
			lastVerticalArrow = ArrowKey.None;
		if(lastVerticalArrow == ArrowKey.Down && Input.GetAxis(verticalButtonName) > -0.1f)
			lastVerticalArrow = ArrowKey.None;
		if(lastHorizontalArrow == ArrowKey.Right && Input.GetAxis(horizontalButtonName) < 0.1f)
			lastHorizontalArrow = ArrowKey.None;
		if(lastHorizontalArrow == ArrowKey.Left && Input.GetAxis(horizontalButtonName) > -0.1f)
			lastHorizontalArrow = ArrowKey.None;
	}

	//========================================================================
	//Called by other scripts
	//========================================================================
	public static void SetButtonName(string horizontal, string vertical)
	{
		horizontalButtonName = horizontal;
		verticalButtonName = vertical;
		//reset state of Menu input Manager
		timer = 0;
		lastVerticalArrow = ArrowKey.None;
		lastHorizontalArrow = ArrowKey.None;
	}
	//check if up button is pressed
	public static bool CheckUp()
	{
		//first see if player pressed required button
		if(Input.GetAxis(verticalButtonName) > 0.1f)
		{
			return CheckMove(ArrowKey.Up);
		}else
			return false;
	}

	//check if down button is pressed
	public static bool CheckDown()
	{
		//first see if player pressed required button
		if(Input.GetAxis(verticalButtonName) < -0.1f)
		{
			return CheckMove(ArrowKey.Down);
		}else
			return false;
	}

	//check if right button is pressed
	public static bool CheckRight()
	{
		//first see if player pressed required button
		if(Input.GetAxis(horizontalButtonName) > 0.1f)
		{
			return CheckMove(ArrowKey.Right);
		}else
			return false;
	}

	//check if up button is pressed
	public static bool CheckLeft()
	{
		//first see if player pressed required button
		if(Input.GetAxis(horizontalButtonName) < -0.1f)
		{
			return CheckMove(ArrowKey.Left);
		}else
			return false;
	}

	//check if the pressed key should result in moveing
	private static bool CheckMove(ArrowKey currentArrow)
	{
		//when pressing a different key or enough time past
		if((currentArrow != lastHorizontalArrow && currentArrow!= lastVerticalArrow) || (timer > timeInterval))
		{
			SetStateAfterMove(currentArrow);
			return true;
		}else
			return false;
	}

	//set stat after move towards a direction
	private static void SetStateAfterMove(ArrowKey currentArrow)
	{
		//vertical move
		if((currentArrow == ArrowKey.Up) || (currentArrow == ArrowKey.Down))
			lastVerticalArrow = currentArrow;
		else
			lastHorizontalArrow = currentArrow;
		timer = 0;
	}

}
