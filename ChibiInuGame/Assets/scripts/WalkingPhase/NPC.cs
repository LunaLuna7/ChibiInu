using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public GameObject startDialouge;
    //public GameObject dialogueBubble;
    public DialogueTrigger trigger;
	// Use this for initialization
	void Start () {
        trigger = gameObject.GetComponent<DialogueTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        startDialouge.SetActive(true);

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            startDialouge.SetActive(false);
            trigger.TriggerDialogue();
            //dialogueBubble.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        startDialouge.SetActive(false);    
    }
}
