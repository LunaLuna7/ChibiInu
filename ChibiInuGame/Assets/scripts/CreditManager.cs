using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour {
	public GameObject contentUI;
	public float length;
	private float pastLength = 0;
	public float speed;
	private float scaledSpeed;
	// Use this for initialization
	void Start () {
		scaledSpeed =  speed * Screen.height /1080;
	}
	
	// Update is called once per frame
	void Update () {
		if(pastLength < length)
		{
			if(Input.GetButton("Submit"))
			{
				contentUI.transform.position += new Vector3(0, scaledSpeed * 5, 0);
				pastLength += speed * 5; //we don't need to scale pastLength, since we didn't scale length
			}
			else{
				contentUI.transform.position += new Vector3(0, scaledSpeed, 0);
				pastLength += speed; //we don't need to scale pastLength, since we didn't scale length
			}
		}
	}
}
