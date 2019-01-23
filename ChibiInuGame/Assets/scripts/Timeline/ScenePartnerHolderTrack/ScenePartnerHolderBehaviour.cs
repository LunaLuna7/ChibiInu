using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class ScenePartnerHolderBehaviour : PlayableBehaviour {
    public bool showPartners;
    private bool hasWorked = false;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var partnerSceneHolderManager = playerData as ScenePartnerHolder;
        //play dialogue if this track hasn't been playered before
        if(!hasWorked)
        {
            if(showPartners)
            {
                partnerSceneHolderManager.ShowPartners();
            }else{
                partnerSceneHolderManager.HidePartners();
            }
            hasWorked = true;
        }

    }



}
