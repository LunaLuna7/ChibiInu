using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class ScenePartnerHolderClip : PlayableAsset, ITimelineClipAsset {
	public ScenePartnerHolderBehaviour template = new ScenePartnerHolderBehaviour();

	public ClipCaps clipCaps
	{
		get{return ClipCaps.None;}
	}

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<ScenePartnerHolderBehaviour>.Create(graph, template);
	}
}
