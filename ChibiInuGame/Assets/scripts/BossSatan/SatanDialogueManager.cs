using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatanDialogueManager : MonoBehaviour {
	public Text nameText;
    public Text dialogueText;
    public Image speakerImage;
    public Animator animator;
	private Dialogue[] dialogueSequence;

	public void StartPlayDialogues(int startIndex, int endIndex)
	{
		StartCoroutine(PlayDialogues(startIndex, endIndex));
	}

    private IEnumerator PlayDialogues(int startIndex, int endIndex)
    {
		//UI dialogue window appear
        animator.SetBool("IsOpen", true);
		//make sure sentence is in bounf
		if(endIndex >= dialogueSequence.Length)
			endIndex = dialogueSequence.Length - 1;
        for(int x = startIndex; x <= endIndex; ++x)
        {
            //change content
            yield return PlayOneDialogue(dialogueSequence[x]);
            //wait for input
            yield return new WaitUntil(()=>Input.GetButtonDown("Submit"));
        }
		//UI go away
		EndDialogue();
    }

	public IEnumerator PlayDialogues(int startIndex, int endIndex, string dialogueFilePath)
    {
		//add postfix for language
        dialogueFilePath = "English/" + dialogueFilePath;
        //at the start of level, ask dialogue Library to load dialogue data, then get it
        dialogueSequence = DialogueLibrary.instance.LoadDialogueJson(dialogueFilePath.Trim());
		//UI dialogue window appear
        animator.SetBool("IsOpen", true);
		//make sure sentence is in bounf
		if(endIndex >= dialogueSequence.Length)
			endIndex = dialogueSequence.Length - 1;
        for(int x = startIndex; x <= endIndex; ++x)
        {
            //change content
            yield return PlayOneDialogue(dialogueSequence[x]);
            //wait for input
            yield return new WaitUntil(()=>Input.GetButtonDown("Submit"));
        }
		//UI go away
		EndDialogue();
    }


    IEnumerator PlayOneDialogue(Dialogue dialogue)
    {
		//change name and sprite
		//show player's name
		if(dialogue.speakerName == "player")
			nameText.text = SaveManager.dataInUse.playerName;
		else
			nameText.text = DialogueLibrary.instance.GetName(dialogue.speakerName);
		speakerImage.sprite = DialogueLibrary.instance.GetFaceSprite(dialogue.imageName);
		//show text
        dialogueText.text = "";
        for(int x = 1; x< dialogue.sentence.Length; ++x)
        {
            dialogueText.text = dialogue.sentence.Substring(0, x) + "<color=#ffffff00>"+ dialogue.sentence.Substring(x)+"</color>";
            yield return null;
        }
        dialogueText.text = dialogue.sentence;
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
	
}
