using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartnerUI : MonoBehaviour {

    public PartnerManager partnerManager;
    private Partner currentPartner;
    public Text partnerName;
    public Text partnerSkillInfo;
    public Image partnerPicture;

	// Use this for initialization
	void Start () {
        currentPartner = partnerManager.allPartners[0];
        partnerName.text = currentPartner.partnerName.ToString();
        partnerPicture.sprite = currentPartner.image;
        partnerSkillInfo.text = currentPartner.skillInfo;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    public void RightArrow()
    {
        int next = (currentPartner.partnerID + 1) % partnerManager.allPartners.Count;
        while (!partnerManager.allPartners[next].unlocked )
        {
            next = (next + 1) % partnerManager.allPartners.Count;
        }
        
        currentPartner = partnerManager.allPartners[next];
        partnerName.text = currentPartner.partnerName.ToString();
        partnerPicture.sprite = currentPartner.image;
        partnerSkillInfo.text = currentPartner.skillInfo;
    }

    public void LeftArrow()
    {
        int next = (currentPartner.partnerID - 1) % partnerManager.allPartners.Count;
        while (!partnerManager.allPartners[next].unlocked)
        {
            next = (next - 1) % partnerManager.allPartners.Count;
        }

        currentPartner = partnerManager.allPartners[next];
        partnerName.text = currentPartner.partnerName.ToString();
        partnerPicture.sprite = currentPartner.image;
        partnerSkillInfo.text = currentPartner.skillInfo;
    }

    public void QSkill()
    {
        partnerManager.AssignQSkillSlot(currentPartner.partnerID);
    }

    public void WSkill()
    {
        partnerManager.AssignWSkillSlot(currentPartner.partnerID);
    }

    public void ESkill()
    {
        partnerManager.AssignESkillSlot(currentPartner.partnerID);
    }
}
