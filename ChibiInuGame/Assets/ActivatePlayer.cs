using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayer : MonoBehaviour {

    public GameObject player;
    public GameObject falsePlayer;
    public GameObject cameraFalsePlayer;
    public GameObject dust;
	// Use this for initialization
	void Start () {
        StartCoroutine(Activate());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(3f);
        dust.SetActive(true);
        yield return new WaitForSeconds(1f);
        falsePlayer.SetActive(false);
        player.SetActive(true);
        yield return new WaitForSeconds(1f);
        cameraFalsePlayer.SetActive(false);
    }
}
