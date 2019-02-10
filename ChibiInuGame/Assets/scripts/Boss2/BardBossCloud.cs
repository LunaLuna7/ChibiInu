using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardBossCloud : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public IEnumerator ChangeColorTo(float targetHue, float time)
	{
		//time for each little change
		float timeInterval = 0.02f;
		//calculate the current hue
		float currentH, currentS, currentV; 
		Color.RGBToHSV(spriteRenderer.color, out currentH, out currentS, out currentV);
		//get infomation needed for the coroutine
		float unitNum = time/timeInterval;
		float diffH = targetHue - currentH;
		//change color in certain amount of time
		for(float x = 0; x < unitNum; ++x)
		{
			spriteRenderer.color = Color.HSVToRGB(currentH + diffH * (x/unitNum), currentS, currentV);
			yield return new WaitForSeconds(timeInterval);
		}
		//make sure the end color is correct
		spriteRenderer.color = Color.HSVToRGB(targetHue, currentS, currentV);
	}

}
