using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class ScriptClip : PlayableAsset, ITimelineClipAsset {
	public ScriptBehaviour template = new ScriptBehaviour();

	public ClipCaps clipCaps
	{
		get{return ClipCaps.None;}
	}

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<ScriptBehaviour>.Create(graph, template);
	}
}
