using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public GameObject dialougeBox;
    public DialogueTrigger trigger;
	// Use this for initialization
	void Start () {
        //trigger = gameObject.GetComponent<DialogueTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialougeBox.SetActive(true);

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            dialougeBox.SetActive(false);
            //Debug.Log(trigger.dialogue.name);
            //trigger.TriggerDialogue();
            
            Debug.Log("talk");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialougeBox.SetActive(false);    
    }
}
