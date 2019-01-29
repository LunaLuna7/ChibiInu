using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

[System.Serializable]
public class ScriptBehaviour : PlayableBehaviour {
    public int[] eventIndexs;
    private bool hasWorked = false;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var script = playerData as FunctionHolder;
        //play dialogue if this track hasn't been playered before
        if(!hasWorked)
        {
            foreach(int index in eventIndexs)
                script.myEvents[index].Invoke();
            hasWorked = true;
        }

    }



}
