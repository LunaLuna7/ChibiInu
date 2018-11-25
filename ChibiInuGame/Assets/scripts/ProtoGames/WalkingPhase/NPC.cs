using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public GameObject startDialouge;
    public DialogueTrigger trigger;
    public bool conversationFinish;
    public float delayTime;
	// Use this for initialization
	void Start () {
        conversationFinish = false;
        trigger = gameObject.GetComponent<DialogueTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(!conversationFinish && collision.gameObject.tag == "Player")
        {
            conversationFinish = true;
            StartCoroutine(Delay());
        }
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        trigger.TriggerDialogue();
    }
}
