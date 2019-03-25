﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class CutSceneDialogueManager : MonoBehaviour {
	public Text nameText;
    public Text dialogueText;
    public Image speakerImage;
    public Animator animator;

	public PlayableDirector playableDirector;
	public string dialogueFilePath;
    [SerializeField] private Dialogue[] dialogueSequence;

    private bool playingDialogue = false;
	void Start()
	{
		//get the dialogue sequences at the beginning
		//add postfix for language
        dialogueFilePath = "English/" + dialogueFilePath;
        //at the start of level, ask dialogue Library to load dialogue data, then get it
        dialogueSequence = DialogueLibrary.instance.LoadDialogueJson(dialogueFilePath.Trim());

	}

    void Update()
    {
        if(Input.GetButtonDown("Skip") && playingDialogue)
        {
            SkipDialogue();
        }
    }

	public void StartPlayDialogues(int startIndex, int endIndex)
	{
		StartCoroutine(PlayDialogues(startIndex, endIndex));
	}

    private IEnumerator PlayDialogues(int startIndex, int endIndex)
    {
        playingDialogue = true;
		//UI dialogue window appear
        animator.SetBool("IsOpen", true);
		//pause timeline
		playableDirector.Pause();
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
        playingDialogue = false;
        animator.SetBool("IsOpen", false);
        //continue the timeline
		playableDirector.Resume();
    }

    public void SkipDialogue()
    {
        StopAllCoroutines();
        EndDialogue();
    }

}
