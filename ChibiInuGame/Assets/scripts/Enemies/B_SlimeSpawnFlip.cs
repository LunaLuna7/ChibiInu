using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_SlimeSpawnFlip : MonoBehaviour {

    public StateController stateController;
    public int currentState;
    public bool m_facingRight = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(currentState);
        if(currentState != stateController.nextPatrolLocation)
        {
            currentState = stateController.nextPatrolLocation;

            if (m_facingRight)
            {
                m_facingRight = !m_facingRight;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (!m_facingRight)
            {
                m_facingRight = !m_facingRight;
                transform.rotation = new Quaternion(0, 0, 180, 0);
            }

        }

        
          
	}
}
