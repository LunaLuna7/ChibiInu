using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

[System.Serializable]
public class ScriptBehaviour : PlayableBehaviour {
    private bool hasWorked = false;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var script = playerData as FunctionHolder;
        //play dialogue if this track hasn't been playered before
        if(!hasWorked)
        {
            script.myEvent.Invoke();
            hasWorked = true;
        }

    }



}
