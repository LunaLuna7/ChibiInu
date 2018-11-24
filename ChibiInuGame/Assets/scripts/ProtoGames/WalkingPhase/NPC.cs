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
        inConversation = false;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
       
        if(collision.gameObject.tag == "Player")
        {
            inConversation = true;
            trigger.TriggerDialogue();
        }
        
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
    }
}
