using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour {


    public PlayableDirector playableDirector;
    public CharacterController2D characterController2D;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playableDirector.Play();
            characterController2D.m_Paralyzed = true;
            //StartCoroutine(StopAnimation());
        }
    }
    
    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(4f);
        playableDirector.Pause();
    }
}
