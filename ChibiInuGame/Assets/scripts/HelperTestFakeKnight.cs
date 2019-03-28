using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperTestFakeKnight : MonoBehaviour {

    public CharacterController2D characterController2D;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            characterController2D.FakeShieldOn();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            characterController2D.FakeShieldOff();
        }
	}
}
