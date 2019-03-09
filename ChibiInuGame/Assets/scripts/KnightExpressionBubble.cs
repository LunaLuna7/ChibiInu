using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightExpressionBubble : MonoBehaviour {

    StateController stateController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool CheckIfChasing()
    {
        return stateController.currentState.sceneGizmoColor == Color.red;
    }
}
