using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogueBehaviour : PlayableBehaviour {
    [SerializeField] private int startDialogueIndex;
    [SerializeField] private int endDialogueIndex;
    [SerializeField] private bool hasPlayed = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var dialogueManager = playerData as CutSceneDialogueManager;
        //play dialogue if this track hasn't been playered before
        if(!hasPlayed)
        {
            dialogueManager.StartPlayDialogues(startDialogueIndex, endDialogueIndex);
            hasPlayed = true;
        }

    }



}
