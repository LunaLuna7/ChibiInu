using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class DialogueClip : PlayableAsset, ITimelineClipAsset {
	public DialogueBehaviour template = new DialogueBehaviour();

	public ClipCaps clipCaps
	{
		get{return ClipCaps.None;}
	}

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		return ScriptPlayable<DialogueBehaviour>.Create(graph, template);
	}
}
