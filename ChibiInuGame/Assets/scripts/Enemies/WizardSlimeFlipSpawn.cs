using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSlimeFlipSpawn : MonoBehaviour {

    public StateController stateController;
    public int currentState;
    public bool facingRight;
	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        Debug.Log(currentState);
        if (currentState != stateController.nextPatrolLocation)
        {
            currentState = stateController.nextPatrolLocation;

            if (facingRight)
            {
                facingRight = !facingRight;
                transform.rotation = new Quaternion(0, 0, 180, 0);

            }
            else if (!facingRight)
            {
                facingRight = !facingRight;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }

        }



    }
}
