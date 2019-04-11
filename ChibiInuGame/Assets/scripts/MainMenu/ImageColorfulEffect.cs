using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColorfulEffect : MonoBehaviour {
	private Image image;
	private bool running;
	public float changingSpeed;
	private Color color;
	// Use this for initialization
	void Awake()
	{
		image = GetComponent<Image>();
		running = false;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(running)
		{
			float h,s,v;
			Color.RGBToHSV(color, out h, out s, out v);
			h += Time.deltaTime * changingSpeed;
			color = Color.HSVToRGB(h, 1, 1);
			image.color = color;
		}
	}

	public void StartEffect()
	{
		if(!running)
		{
			running =  true;
			color = Color.white;
		}
	}

	public void StopEffect()
	{
		if(running)
		{
			running = false;
			color = Color.white;
			image.color = color;
		}
	}

}
