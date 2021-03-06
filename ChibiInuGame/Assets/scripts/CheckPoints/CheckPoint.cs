﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public Sprite activeCheckPoint;
    private SpriteRenderer checkPointImage;
    public int newCheckPoint;
    public GameObject book;
    public List<GameObject> enemies;
    public List<GameObject> walls;
    public GameObject CheckPointParticleAura;
    public int previousCheckPoint;
    public GameObject instructionBubble;
    private UIPartnerBook uIPartnerBook;
    public bool SatanCheckPoint;
    public static bool onDialogue;

    private bool onCheckPoint;
    private bool activated;

    public void ResetingLevel()
    {
        if (newCheckPoint == UpdateCheckPoint.currentCheckPoint)
        {
            for (int i = 0; i < enemies.Count; ++i)
            {
                
                enemies[i].SetActive(true);
            }
        }
    }


    public void Start()
    {
        uIPartnerBook = book.GetComponent<UIPartnerBook>();
        instructionBubble.SetActive(false);
        checkPointImage = GetComponent<SpriteRenderer>();
        activated = false;
        
    }

    public void Update()
    {
        if (onCheckPoint && !SatanCheckPoint && !onDialogue)
        {
            if (Input.GetKeyDown(KeyCode.B) && !book.activeSelf)
            {
                book.SetActive(true);
                uIPartnerBook.openWithKeyboard = true;
                uIPartnerBook.openWithXbox = false;
            }

            else if(Input.GetButtonDown("CallBook") && !book.activeSelf){
                book.SetActive(true);
                uIPartnerBook.openWithKeyboard = false;
                uIPartnerBook.openWithXbox = true;
            }

            else if ((Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("CallBook") || Input.GetButtonDown("Back")) && book.activeSelf)
                book.SetActive(false);
        }
    }

    public void SetCheckPointTo()
    {
        previousCheckPoint = UpdateCheckPoint.currentCheckPoint;
        UpdateCheckPoint.currentCheckPoint = newCheckPoint;
        if (previousCheckPoint > UpdateCheckPoint.currentCheckPoint)
            UpdateCheckPoint.currentCheckPoint = previousCheckPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(uIPartnerBook.partnerManager.partners[0].unlocked && !SatanCheckPoint)
                instructionBubble.SetActive(true);
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if(health != null)
                health.HealDamage();
            SetCheckPointTo();
            onCheckPoint = true;
            checkPointImage.sprite = activeCheckPoint;
            SoundEffectManager.instance.Play("CheckPointAura");
            
            if (!activated)
            {
                CheckPointParticleAura.SetActive(true);
                SoundEffectManager.instance.Play("CheckPoint");
                activated = true;
            }

            RespawnWalls();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && (uIPartnerBook.partnerManager.partners[0].unlocked) && !onDialogue && !SatanCheckPoint)
            instructionBubble.SetActive(true);
    }



    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            instructionBubble.SetActive(false);
            SoundEffectManager.instance.Stop("CheckPointAura");
            onCheckPoint = false;
            book.SetActive(false);
            RespawnWalls();
        }
       
    }

    void RespawnWalls()
    {
        foreach(GameObject each in walls)
            each.SetActive(true);
    }
}
