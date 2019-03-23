using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour {
	public GameObject contentUI;
	public Image curtain;
	public float length;
	private float pastLength = 0;
	public float speed;
	private float scaledSpeed;

	private bool started;
	private bool finished;
	// Use this for initialization
	void Start () {
		scaledSpeed =  speed * Screen.height /1080;
		finished = false;
		started = false;
		StartCoroutine(StartCredit(1));
		//switch music
		SoundEffectManager.instance.Stop("MainMenu");
		SoundEffectManager.instance.Stop("SatanBattle");
		SoundEffectManager.instance.Play("OverWorld");
	}
	
	// Update is called once per frame
	void Update () {
		if(pastLength < length && started)
		{
			//move the text
			if(Input.GetButton("Submit"))
			{
				contentUI.transform.position += new Vector3(0, scaledSpeed * 5, 0);
				pastLength += speed * 5; //we don't need to scale pastLength, since we didn't scale length
			}
			else{
				contentUI.transform.position += new Vector3(0, scaledSpeed, 0);
				pastLength += speed; //we don't need to scale pastLength, since we didn't scale length
			}
		}else if(pastLength >= length && !finished)
		{
			StartCoroutine(EndCredit(1));
			finished = true;
		}
	}

	private IEnumerator StartCredit(float time)
	{
		/* 
		Color color = curtain.color;
		for(float x = 1; x >=0; x -= Time.deltaTime/time)
		{
			color.a = x;
			curtain.color = color;
			yield return new WaitForEndOfFrame();
		}
		color.a = 0;
		curtain.color = color;*/
		yield return new WaitForSeconds(time);
		started = true;
	}

	private IEnumerator EndCredit(float time)
	{
		yield return new WaitForSeconds(time);
		Color color = curtain.color;
		for(float x = 0; x <=1; x += Time.deltaTime/time)
		{
			color.a = x;
			curtain.color = color;
			yield return new WaitForEndOfFrame();
		}
		color.a = 1;
		curtain.color = color;
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}

}
