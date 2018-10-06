using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour {


    private SwitchCamera sw;
    public int EnterCamera;
    public int ExitCamera;


	void Start () {
        sw = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SwitchCamera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sw.ChangeCamera(EnterCamera);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sw.ChangeCamera(ExitCamera);
        }
    }
}
