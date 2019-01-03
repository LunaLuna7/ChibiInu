using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogueBehaviour : PlayableBehaviour {
    [SerializeField] private int startDialogueIndex;
    [SerializeField] private int endDialogueIndex;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var dialogueManager = playerData as DialogueManager;
        
    }

}
