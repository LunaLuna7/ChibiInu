using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {


    public DialogueTrigger trigger;
	// Use this for initialization
	void Start () {
        trigger = gameObject.GetComponent<DialogueTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(trigger.dialogue.name);
            trigger.TriggerDialogue();
            Debug.Log("talk");
        }
    }
}
