using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public GameObject startDialouge;
    public DialogueTrigger trigger;
    public bool inConversation;
	// Use this for initialization
	void Start () {
        inConversation = false;
        trigger = gameObject.GetComponent<DialogueTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        startDialouge.SetActive(true);
        inConversation = false;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.X) && inConversation == false)
        {
            inConversation = true;
            startDialouge.SetActive(false);
            trigger.TriggerDialogue();
            //dialogueBubble.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        startDialouge.SetActive(false);
        trigger.EndDialogue();
    }
}
