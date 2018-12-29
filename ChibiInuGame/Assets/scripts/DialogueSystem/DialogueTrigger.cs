using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public string dialogueFilePath;

    void Start()
    {
        //at the start of level, ask dialogue Library to load dialogue data
        DialogueLibrary.instance.LoadDialogueJson(dialogueFilePath);
    }

    public void TriggerDialogue()
    {
        Dialogue[] dialogueSequence = DialogueLibrary.instance.GetDialogueSequence(dialogueFilePath);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueSequence);
    }

    public void EndDialogue()
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
    }
}
