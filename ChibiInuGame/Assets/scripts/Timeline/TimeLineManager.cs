using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour {


    public PlayableDirector playableDirector;
    public CharacterController2D characterController2D;
    public Animator playerAnimator;
    public bool conversationFinish = false;
    public bool resumeMovementAfterCutscene = true;
 
    // Use this for initialization
    void Start () {
        playableDirector.stopped += OnTimelineStopped;
	}
	
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!conversationFinish && collision.gameObject.tag == "Player")
        {
            Play();
        }
    }
    
    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(4f);
        playableDirector.Pause();
    }

    public void OnTimelineStopped(PlayableDirector aDirector)
    {
        if(playableDirector == aDirector && resumeMovementAfterCutscene)
            characterController2D.m_Paralyzed = false;
    }

    public void Play()
    {
            playableDirector.Play();
            characterController2D.m_Paralyzed = true;

            conversationFinish = true;
    }

    public void PlayerEnterTimeline()
    {
        playerAnimator.Play("StandingIdle");
        CheckPoint.onDialogue = true;
    }

    public void PlayerExitTimeline()
    {
        playerAnimator.Play("ShibIdle");
        CheckPoint.onDialogue = false;
    }
}
