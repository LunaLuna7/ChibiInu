using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardBossCloud : MonoBehaviour {
	private SpriteRenderer spriteRenderer;
	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public IEnumerator ChangeColorTo(Color targetColor, float time)
	{
		//time for each little change
		float timeInterval = 0.02f;
		Color currentColor = spriteRenderer.color;
		//calculate the current hue
		//float currentH, currentS, currentV; 
		//Color.RGBToHSV(spriteRenderer.color, out currentH, out currentS, out currentV);
		//get infomation needed for the coroutine
		float unitNum = time/timeInterval;
		float diffR = targetColor.r - currentColor.r;
		float diffG = targetColor.g - currentColor.g;
		float diffB = targetColor.b - currentColor.b;
		//change color in certain amount of time
		for(float x = 0; x < unitNum; ++x)
		{
			spriteRenderer.color = new Color(currentColor.r + diffR * (x/unitNum), currentColor.g + diffG * (x/unitNum), currentColor.b + diffB * (x/unitNum));
			yield return new WaitForSeconds(timeInterval);
		}
		//make sure the end color is correct
		spriteRenderer.color = targetColor;
	}

	public void SetColor(Color targetColor)
	{
		spriteRenderer.color = targetColor;
	}

}
