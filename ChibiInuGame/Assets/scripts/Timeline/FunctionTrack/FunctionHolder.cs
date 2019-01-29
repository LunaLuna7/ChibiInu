using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionHolder : MonoBehaviour {
	public UnityEvent[] myEvents;
	// Use this for initialization
	void Awake () {
		for(int x = 0; x<= myEvents.Length;++x)
		{
			if(myEvents[x] == null)
			{
				myEvents[x] = new UnityEvent();
			}
		}
	}

}
