using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public Image speakerImage;
    public Animator animator;
    public CharacterController2D characterController2D;

    private Queue<Dialogue> dialogueQueue;
	// Use this for initialization
	void Start () {
        dialogueQueue = new Queue<Dialogue>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartDialogue(Dialogue[] dialogueSequence)
    {
        animator.SetBool("IsOpen", true);
        //nameText.text = dialogue.name;

        //put all dialogues to the queue
        dialogueQueue.Clear();
        foreach(Dialogue dialogue in dialogueSequence)
        {
            dialogueQueue.Enqueue(dialogue);
        }
        //DisplayNextSentence();
        StartCoroutine(DisplaySentences());
    }

    /* 
    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();  
        }
        else
        {
            Dialogue dialogue = dialogueQueue.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogue.sentence));
        }
    }*/
    public IEnumerator DisplaySentences()
    {
        while(dialogueQueue.Count > 0)
        {
            //display a single sentence
            Dialogue dialogue = dialogueQueue.Dequeue();
            //change name and sprite
            nameText.text = dialogue.speakerName;
            speakerImage.sprite = DialogueLibrary.instance.GetFaceSprite(dialogue.imageName);
            //change content
            yield return TypeSentence(dialogue.sentence);
            //wait for input
            yield return new WaitUntil(()=>Input.GetButtonDown("Submit"));
        }
        EndDialogue();
    }

    IEnumerator TypeSentence(string sentence)
    {
        
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        characterController2D.m_Paralyzed = false;
    }
}
