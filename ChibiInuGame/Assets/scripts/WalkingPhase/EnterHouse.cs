using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse : MonoBehaviour {

    public GameObject enterSignal;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enterSignal.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("new scene");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enterSignal.SetActive(false);
    }
}
