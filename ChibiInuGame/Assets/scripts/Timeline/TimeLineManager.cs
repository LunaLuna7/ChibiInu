using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour {


    public PlayableDirector playableDirector;
    public CharacterController2D characterController2D;
    public bool conversationFinish = false;
 
    // Use this for initialization
    void Start () {
        playableDirector.stopped += OnTimelineStopped;
	}
	
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!conversationFinish && collision.gameObject.tag == "Player")
        {
            playableDirector.Play();
            characterController2D.m_Paralyzed = true;

            conversationFinish = true;
        }
    }
    
    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(4f);
        playableDirector.Pause();
    }

    public void OnTimelineStopped(PlayableDirector aDirector)
    {
        if(playableDirector == aDirector)
            characterController2D.m_Paralyzed = false;
    }
}
