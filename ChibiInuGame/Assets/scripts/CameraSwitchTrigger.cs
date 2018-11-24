using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour {


    private SwitchCamera sw;
    public int EnterCamera;
    public int ExitCamera;
    public CharacterController2D characterController2D;

	void Start () {
        sw = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SwitchCamera>();
        characterController2D = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            characterController2D.m_OnOtherCamera = true;
            Debug.Log("HERE");
            sw.ChangeCamera(EnterCamera);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            characterController2D.m_OnOtherCamera = false;
            sw.ChangeCamera(ExitCamera);
        }
    }
}
