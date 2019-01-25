using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionHolder : MonoBehaviour {
	public UnityEvent myEvent;
	// Use this for initialization
	void Awake () {
		if(myEvent == null)
		{
			myEvent = new UnityEvent();
		}
	}

}
