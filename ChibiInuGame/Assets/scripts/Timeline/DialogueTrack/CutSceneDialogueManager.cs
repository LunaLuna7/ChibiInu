using System.Collections;
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

    //[Header("Skip")]
    private bool playingDialogue = false;
    public Text skipText;
	void Start()
	{
		//get the dialogue sequences at the beginning
		//add postfix for language
        dialogueFilePath = "English/" + dialogueFilePath;
        //at the start of level, ask dialogue Library to load dialogue data, then get it
        dialogueSequence = DialogueLibrary.instance.LoadDialogueJson(dialogueFilePath.Trim());
        //hide skip text
        //Color currentColor = skipText.color;
        //currentColor.a = 0;
        //skipText.color = currentColor;

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
        StartCoroutine(AnimateSkipText());
        EndDialogue();
    }

    private IEnumerator AnimateSkipText()
    {
        //show skip text
        Color currentColor = skipText.color;
        currentColor.a = 1;
        skipText.color = currentColor;
        yield return new WaitForSeconds(0.5f);
        //hide the text
        float unit = 1/0.5f; //   1/time
        for(float x = 1; x >= 0;x -= unit * Time.deltaTime)
        {
            currentColor.a = x;
            skipText.color = currentColor;
            yield return new WaitForEndOfFrame();
        }
        currentColor.a = 0;
        skipText.color = currentColor;
    }

}
