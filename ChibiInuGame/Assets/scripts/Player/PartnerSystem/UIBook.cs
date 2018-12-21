﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBook : MonoBehaviour {

    public PartnerManager partnerManager;

    [HideInInspector]public Partner currentPartner;
    public Text partnerName;
    public Image partnerPicture;
    public Text partnerSkillInfo;
    public Button rightArrow;
    public Button leftArrow;

    public List<GameObject> partnerBookMarks;
    public List<GameObject> partnerLightMark;
    public CharacterController2D characterController;
    private SkillSummonUI skillSummonUI;

    private void Start()
    {
        skillSummonUI = GetComponent<SkillSummonUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            LeftArrow();
        
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            RightArrow();

        if (Input.GetKeyDown(KeyCode.J))
        {
            skillSummonUI.SummonPartner("J");

        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            skillSummonUI.SummonPartner("K");
        }
    }

    void OnEnable()
    {
        characterController.m_Paralyzed = true;
        CheckUnlockedPartners();
        if (partnerManager.allPartners[0].unlocked)
        {
            currentPartner = partnerManager.allPartners[0];
            UpdatePartnerPage(currentPartner.partnerID);
            EnableBook();
        }
        else
        {
            DisableBook();
        }
    }

    private void OnDisable()
    {
        characterController.m_Paralyzed = false;

        bool noPartner = true;
        for (int i = 0; i < partnerManager.allPartners.Capacity; ++i)
        {
            if (partnerManager.allPartners[i].selected)
                noPartner = false;
        }
        if (noPartner)
        {
            characterController.m_AirJumps = 2;
        }
        else
        {
            characterController.m_AirJumps = 1;
        }
    }

    public void RightArrow()
    {
        int nextPartner = (currentPartner.partnerID + 1) % partnerManager.allPartners.Count;
        while (!partnerManager.allPartners[nextPartner].unlocked)
        {
            nextPartner = (nextPartner + 1) % partnerManager.allPartners.Count;
        }

        UpdatePartnerPage(nextPartner);
    }

    public void LeftArrow()
    {
        int nextPartner;

        if (currentPartner.partnerID == 0)
            nextPartner = partnerManager.allPartners.Count -1;
        else
            nextPartner = (currentPartner.partnerID - 1) % partnerManager.allPartners.Count;
      
        while (!partnerManager.allPartners[nextPartner].unlocked)
        {
            nextPartner = (nextPartner - 1) % partnerManager.allPartners.Count;
        }

        UpdatePartnerPage(nextPartner);
    }

    public void ShortCutPartner(int nextPartner)
    {
        UpdatePartnerPage(nextPartner);
    }

    private void UpdatePartnerPage(int nextPartner)
    {
        partnerLightMark[currentPartner.partnerID].SetActive(false);
        currentPartner = partnerManager.allPartners[nextPartner];
        partnerLightMark[currentPartner.partnerID].SetActive(true);
        partnerName.text = currentPartner.partnerName.ToString();
        partnerPicture.sprite = currentPartner.image;
        partnerSkillInfo.text = currentPartner.skillInfo;

    }

    private void DisableBook()
    {
        partnerName.enabled = false;
        partnerPicture.enabled = false;
        partnerSkillInfo.enabled = false;
        rightArrow.enabled = false;
        leftArrow.enabled = false;
    }

    private void EnableBook()
    {
        partnerName.enabled = true;
        partnerPicture.enabled = true;
        partnerSkillInfo.enabled = true;
        rightArrow.enabled = true;
        leftArrow.enabled = true;

    }

    private void CheckUnlockedPartners()
    {
        for(int i = 0; i < partnerManager.allPartners.Count; ++i)
        {
            if (partnerManager.allPartners[i].unlocked)
            {
                partnerBookMarks[i].SetActive(true);
            }
            else
            {
                partnerBookMarks[i].SetActive(false);
            }
        }
    }


}
