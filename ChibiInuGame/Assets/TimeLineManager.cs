using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour {


    public PlayableDirector playableDirector;
    public CharacterController2D characterController2D;
    public bool conversationFinish;
    // Use this for initialization
    void Start () {
        conversationFinish = false;
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
