using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public string dialogueFilePath;

    void Start()
    {
        //add postfix for language
        dialogueFilePath = "English/" + dialogueFilePath;
        //at the start of level, ask dialogue Library to load dialogue data
        DialogueLibrary.instance.LoadDialogueJson(dialogueFilePath.Trim());
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
