using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


//change the configuration of the pixel perfect camera
[RequireComponent(typeof(PixelPerfectCamera))]
public class CameraPixelConfiguration : MonoBehaviour {
	private PixelPerfectCamera ppCamera;
	public Vector2 basedResolution = new Vector2(1920, 1080);
	public int basedAPPU = 27;
	private Vector2 currentResolution;

	void Awake () {
		ppCamera = GetComponent<PixelPerfectCamera>();
	}

	void Start()
	{
		currentResolution = new Vector2(Screen.width, Screen.height);
		ModifyConfiguration(currentResolution);
	}
	
	// Update is called once per frame
	void Update () {
		if(currentResolution != new Vector2(Screen.width, Screen.height))
		{
			currentResolution = new Vector2(Screen.width, Screen.height);
			ModifyConfiguration(currentResolution);
		}
	}

	public void ModifyConfiguration(Vector2 resolution)
	{
		ppCamera.refResolutionX = (int)resolution.x;
		ppCamera.refResolutionY = (int)resolution.y;
		//calculate the new APPU
		float ratioW = resolution.x / basedResolution.x;
		float ratioH = resolution.y / basedResolution.y;
		int newAPPU = (int)(basedAPPU * (ratioH + ratioW)/2 + 0.5);
		ppCamera.assetsPPU = newAPPU;
	}

}
