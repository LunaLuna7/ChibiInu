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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdatePartnerPage(int nextPartner)
    {
        currentPartner = partnerManager.allPartners[nextPartner];
        partnerName.text = currentPartner.name.ToString();
        partnerPicture.sprite = currentPartner.partnerInfo.image;
        partnerSkillInfo.text = currentPartner.skillInfo;

    }
}
