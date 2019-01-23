using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPartnerBook : MonoBehaviour {

    public PartnerManager partnerManager;
    [HideInInspector] public Partner currentPartner;
    public CharacterController2D characterController; //maybe
    

    public Text partnerName;
    public Image partnerPicture;
    public Text partnerSkillInfo;
    public Button rightArrow;
    public Button leftArrow;

    private void UpdatePartnerPage(int nextPartner)
    {
        currentPartner = partnerManager.partners[nextPartner];
        partnerName.text = currentPartner.name.ToString();
        partnerPicture.sprite = currentPartner.partnerInfo.image;
        partnerSkillInfo.text = currentPartner.partnerInfo.skillInfo;

    }

    void OnEnable()
    {
        characterController.m_Paralyzed = true;
        if (partnerManager.partners[0].unlocked)
        {
            currentPartner = partnerManager.partners[0];
            UpdatePartnerPage(currentPartner.partnerInfo.partnerId);
            EnableBook();
        }
        else
            DisableBook();
        
    }

    private void OnDisable()
    {
        characterController.m_Paralyzed = false;
    }

    //Moves right(+1 index) through the partnerManager.cs allPartners list of Scriptable Partners objects and if unlock it updats the page and currentPartner
    public void RightArrow()
    {
        int nextPartner = (currentPartner.partnerInfo.partnerId + 1) % partnerManager.partners.Count;
        while (!partnerManager.partners[nextPartner].unlocked)
        {
            nextPartner = (nextPartner + 1) % partnerManager.partners.Count; //makes it so we dont indexOutOfBound and loops back through list index
        }

        UpdatePartnerPage(nextPartner);
    }

    //Moves Left(-1 index) through the partnerManager.cs allPartners list of Scriptable Partners objects and if unlock it updats the page and currentPartner
    public void LeftArrow()
    {
        int nextPartner;

        if (currentPartner.partnerInfo.partnerId == 0)
            nextPartner = partnerManager.partners.Count - 1;
        else
            nextPartner = (currentPartner.partnerInfo.partnerId - 1) % partnerManager.partners.Count; //makes it so we dont indexOutOfBound and loops back through list index

        while (!partnerManager.partners[nextPartner].unlocked)
        {
            nextPartner = (nextPartner - 1) % partnerManager.partners.Count;
        }

        UpdatePartnerPage(nextPartner);
    }

    //Hide the partnerWindow in canvas
    private void DisableBook()
    {
        gameObject.SetActive(false);
    }

    //tunr on all UI book elements
    private void EnableBook()
    {
        gameObject.SetActive(true);
    }

}
