using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightExpressionBubble : MonoBehaviour {

    StateController stateController;
    public GameObject spotted;
    public GameObject lost;
    bool playerLost;
    bool playerFound;

	// Use this for initialization
	void Awake () {
        stateController = GetComponentInParent<StateController>();
        playerLost = true;
        playerFound = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Chasing() && playerLost)
        {
            //StopAllCoroutines();
            StartCoroutine(ActivateBubble());
            playerLost = false;
            playerFound = true;
        }
        else if(!Chasing() && playerFound)
        {
            // StopAllCoroutines();
            StartCoroutine(ActivateLost());
            playerLost = true;
            playerFound = false;
        }
	}

    bool Chasing()
    {
        return stateController.playerInRange && !stateController.player.GetComponent<CharacterController2D>().m_OnShield;
    }

    IEnumerator ActivateBubble()
    {
        spotted.SetActive(true);
        SoundEffectManager.instance.Play("exclamation_expression");
        yield return new WaitForSeconds(1f);
        spotted.SetActive(false);
    }

    IEnumerator ActivateLost()
    {
        lost.SetActive(true);
        SoundEffectManager.instance.Play("question_expression");
        yield return new WaitForSeconds(1f);
        lost.SetActive(false);
    }
}
