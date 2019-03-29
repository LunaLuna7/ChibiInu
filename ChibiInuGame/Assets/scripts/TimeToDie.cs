using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDie : MonoBehaviour {

    public float aliveTime = 2f;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, aliveTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
