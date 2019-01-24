    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBook : MonoBehaviour {
    /// <summary>
    /// UIBook.cs hadles all the partners info(picture, name, description) in the UI of the Book. It checks if partners are unlock 
    /// and if so it shows them in the UI
    /// </summary>


    public PartnerManager partnerManager;
    [HideInInspector]public Partner currentPartner; //The scriptable Obj partner we are currently reading data from

    //====All the UI elements in the book that will be updated to the Scriptable Obj partner data
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

    //Called when UI Book is openned, checks if first partner unlock if so it UpdatesPage and current partner
    void OnEnable()
    {
        characterController.m_Paralyzed = true;
        CheckUnlockedPartners();
        if (partnerManager.partners[0].unlocked)
        {
            currentPartner = partnerManager.partners[0];
            UpdatePartnerPage(currentPartner.partnerID);
            EnableBook();
        }
        else
        {
            DisableBook();
        }
    }

    //called after book is closed, it checks if any partner is selected. If not then player gets 3 jumps
    private void OnDisable()
    {
        characterController.m_Paralyzed = false;

        bool noPartner = true;
        for (int i = 0; i < partnerManager.partners.Capacity; ++i)
        {
            if (partnerManager.partners[i].selected)
                noPartner = false;
        }
        if (noPartner)
            characterController.m_AirJumps = 2;
        
        else
            characterController.m_AirJumps = 1;
        
    }


    //Moves right(+1 index) through the partnerManager.cs allPartners list of Scriptable Partners objects and if unlock it updats the page and currentPartner
    public void RightArrow()
    {
        int nextPartner = (currentPartner.partnerID + 1) % partnerManager.partners.Count;
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

        if (currentPartner.partnerID == 0)
            nextPartner = partnerManager.partners.Count -1;
        else
            nextPartner = (currentPartner.partnerID - 1) % partnerManager.partners.Count; //makes it so we dont indexOutOfBound and loops back through list index

        while (!partnerManager.partners[nextPartner].unlocked)
        {
            nextPartner = (nextPartner - 1) % partnerManager.partners.Count;
        }

        UpdatePartnerPage(nextPartner);
    }

    public void ShortCutPartner(int nextPartner)
    {
        UpdatePartnerPage(nextPartner);
    }

    //Updates the UI page base on currentPartner
    private void UpdatePartnerPage(int nextPartner)
    {
        partnerLightMark[currentPartner.partnerID].SetActive(false);
        currentPartner = partnerManager.partners[nextPartner];
        partnerLightMark[currentPartner.partnerID].SetActive(true);
        partnerName.text = currentPartner.partnerName.ToString();
        partnerPicture.sprite = currentPartner.image;
        partnerSkillInfo.text = currentPartner.skillInfo;

    }

    //turns off all UI book elements
    private void DisableBook()
    {
        partnerName.enabled = false;
        partnerPicture.enabled = false;
        partnerSkillInfo.enabled = false;
        rightArrow.enabled = false;
        leftArrow.enabled = false;
    }

    //tunr on all UI book elements
    private void EnableBook()
    {
        partnerName.enabled = true;
        partnerPicture.enabled = true;
        partnerSkillInfo.enabled = true;
        rightArrow.enabled = true;
        leftArrow.enabled = true;

    }

    //CHecks if partner is unlocked based on the scriptable Obj
    private void CheckUnlockedPartners()
    {
        for(int i = 0; i < partnerManager.partners.Count; ++i)
        {
            if (partnerManager.partners[i].unlocked)
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
