using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIPartnerBook controls the Canvas UI Book and updates the book's pages depending on user input and partner's info
/// </summary>

    //TODO: make your own button active and deactivate based on if the partner is in used and update page accordingle!!!
public class UIPartnerBook : MonoBehaviour {

    public PartnerManager partnerManager;
    [HideInInspector] public Partner currentPartner;
    public CharacterController2D characterController;

    [Space]
    [Header("Book Page UI Elements")]
    public Text partnerName;
    public Image partnerPicture;
    public Text partnerSkillInfo;
    public Button rightArrow;
    public Button leftArrow;
    public GameObject firstPartnerButtonSummon;
    public GameObject secondPartnerButtonSummon;
    public GameObject callBackPartnerButton;
    


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            LeftArrow();

        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            RightArrow();
    }

        //Updates the individual UI objects on teh scene canvas in respect to the SCriptable Object of the current partner
    private void UpdatePartnerPage(int nextPartner)
    {
        SoundEffectManager.instance.Play("PageFlip");
        currentPartner = partnerManager.partners[nextPartner];
        partnerName.text = currentPartner.partnerInfo.name.ToString();
        partnerPicture.sprite = currentPartner.partnerInfo.image;
        partnerSkillInfo.text = currentPartner.partnerInfo.skillInfo;

        if (!currentPartner.inUse)
        {
            callBackPartnerButton.SetActive(false);
            firstPartnerButtonSummon.SetActive(true);
            if(partnerManager.secondPartnerSlotUnlock)
                secondPartnerButtonSummon.SetActive(true);
        }
        else
        {
            callBackPartnerButton.SetActive(true);
            firstPartnerButtonSummon.SetActive(false);
            secondPartnerButtonSummon.SetActive(false);
        }


    }

    //Checks if there is at least the first player unlocked. If so then activates the Book on Canvas. Also freezes player movement
    void OnEnable()
    {
        characterController.m_Paralyzed = true;
        if (partnerManager.partners[0].unlocked)
        {
            SoundEffectManager.instance.Play("OpenBook");
            currentPartner = partnerManager.partners[0];
            UpdatePartnerPage(currentPartner.partnerInfo.partnerId);
            if (partnerManager.secondPartnerSlotUnlock)
                secondPartnerButtonSummon.SetActive(true);
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }

    //Unfreeze player
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
            nextPartner = (nextPartner - 1) % partnerManager.partners.Count;
        
        UpdatePartnerPage(nextPartner);
    }

    //Button press on UI book that will call summonPartner from partnerManager with the currentpartner and skillslot base on 0 or 1 info as param
    public void SummonPartnerButton(int keySlot)
    {
        SoundEffectManager.instance.Play("PartnerSelect");
        if (keySlot == 0)
            partnerManager.SummonPartner(SkillSlot.FirstSlot, currentPartner);
        else
            partnerManager.SummonPartner(SkillSlot.SecondSlot, currentPartner);

        partnerManager.PartnerInUse(currentPartner);
        firstPartnerButtonSummon.SetActive(false);
        secondPartnerButtonSummon.SetActive(false);
        callBackPartnerButton.SetActive(true);
        partnerManager.LimitPlayerJump(partnerManager.TripleJumpPartnerCapacity());
    }

    //called when callback UI is press(ToDO: make it so either summonA button appears or summonB and summonA appears)
    public void UnSummonPartnerButton()
    {
        SoundEffectManager.instance.Play("PartnerDeselect");
        partnerManager.UnSummonPartner(currentPartner);
        callBackPartnerButton.SetActive(false);
        firstPartnerButtonSummon.SetActive(true);
        if(partnerManager.secondPartnerSlotUnlock)
            secondPartnerButtonSummon.SetActive(true);

        partnerManager.LimitPlayerJump(partnerManager.TripleJumpPartnerCapacity());
    }
}
