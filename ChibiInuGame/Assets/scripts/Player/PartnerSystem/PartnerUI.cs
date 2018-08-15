using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartnerUI : MonoBehaviour {

    public PartnerManager partnerManager;

    private Partner currentLeftPartner;
    public Text partnerLeftName;
    public Image partnerLeftPicture;
    public Text partnerLeftSkillInfo;
   
    [Space]

    private Partner currentRightPartner;
    public Text partnerRightName;
    public Image partnerRightPicture;
    public Text partnerRightSkillInfo;
    

    void Start () {
        currentLeftPartner = partnerManager.allPartners[0];
        partnerLeftName.text = currentLeftPartner.partnerName.ToString();
        partnerLeftPicture.sprite = currentLeftPartner.image;
        partnerLeftSkillInfo.text = currentLeftPartner.skillInfo;

        currentRightPartner = partnerManager.allPartners[1];
        partnerRightName.text = currentRightPartner.partnerName.ToString();
        partnerRightPicture.sprite = currentRightPartner.image;
        partnerRightSkillInfo.text = currentRightPartner.skillInfo;
		
	}
	

    public void RightArrow()
    {
        int leftNext = (currentLeftPartner.partnerID + 2) % partnerManager.allPartners.Count;
        int rightNext = (currentRightPartner.partnerID + 2) % partnerManager.allPartners.Count;

        while (!partnerManager.allPartners[leftNext].unlocked)
        {
            leftNext = (leftNext + 2) % partnerManager.allPartners.Count;
        }

        while (!partnerManager.allPartners[rightNext].unlocked)
        {
            rightNext = (rightNext + 2) % partnerManager.allPartners.Count;
        }

        currentLeftPartner = partnerManager.allPartners[leftNext];
        partnerLeftName.text = currentLeftPartner.partnerName.ToString();
        partnerLeftPicture.sprite = currentLeftPartner.image;
        partnerLeftSkillInfo.text = currentLeftPartner.skillInfo;

        currentRightPartner = partnerManager.allPartners[rightNext];
        partnerRightName.text = currentRightPartner.partnerName.ToString();
        partnerRightPicture.sprite = currentRightPartner.image;
        partnerRightSkillInfo.text = currentRightPartner.skillInfo;
    }

    public void LeftArrow()
    {
        int leftNext = (currentLeftPartner.partnerID - 2) % partnerManager.allPartners.Count;
        int rightNext = (currentRightPartner.partnerID - 2) % partnerManager.allPartners.Count;

        while (!partnerManager.allPartners[leftNext].unlocked)
        {
            leftNext = (leftNext - 2) % partnerManager.allPartners.Count;
        }

        while (!partnerManager.allPartners[rightNext].unlocked)
        {
            rightNext = (rightNext - 2) % partnerManager.allPartners.Count;
        }

        currentLeftPartner = partnerManager.allPartners[leftNext];
        partnerLeftName.text = currentLeftPartner.partnerName.ToString();
        partnerLeftPicture.sprite = currentLeftPartner.image;
        partnerLeftSkillInfo.text = currentLeftPartner.skillInfo;

        currentRightPartner = partnerManager.allPartners[rightNext];
        partnerRightName.text = currentRightPartner.partnerName.ToString();
        partnerRightPicture.sprite = currentRightPartner.image;
        partnerRightSkillInfo.text = currentRightPartner.skillInfo;
    }

    public void QRightSkill()
    {
        partnerManager.AssignQSkillSlot(currentRightPartner.partnerID);
        partnerManager.allPartners[currentRightPartner.partnerID].selected = true;
        partnerManager.partners[currentRightPartner.partnerID].transform.position = partnerManager.partnerSpawnLocations[currentRightPartner.partnerID].position;
        partnerManager.partners[currentRightPartner.partnerID].SetActive(true);
        
    }

    public void WRightSkill()
    {
        partnerManager.AssignWSkillSlot(currentRightPartner.partnerID);
        partnerManager.allPartners[currentRightPartner.partnerID].selected = true;
        partnerManager.partners[currentRightPartner.partnerID].transform.position = partnerManager.partnerSpawnLocations[currentRightPartner.partnerID].position;
        partnerManager.partners[currentRightPartner.partnerID].SetActive(true);
    }

    public void ERightSkill()
    {
        partnerManager.AssignESkillSlot(currentRightPartner.partnerID);
        partnerManager.allPartners[currentRightPartner.partnerID].selected = true;
        partnerManager.partners[currentRightPartner.partnerID].transform.position = partnerManager.partnerSpawnLocations[currentRightPartner.partnerID].position;
        partnerManager.partners[currentRightPartner.partnerID].SetActive(true);
    }

    public void QLeftSkill()
    {
        partnerManager.AssignQSkillSlot(currentLeftPartner.partnerID);
        partnerManager.allPartners[currentLeftPartner.partnerID].selected = true;
        partnerManager.partners[currentLeftPartner.partnerID].transform.position = partnerManager.partnerSpawnLocations[currentLeftPartner.partnerID].position;
        partnerManager.partners[currentLeftPartner.partnerID].SetActive(true);

    }

    public void WLeftSkill()
    {
        partnerManager.AssignWSkillSlot(currentLeftPartner.partnerID);
        partnerManager.allPartners[currentLeftPartner.partnerID].selected = true;
        partnerManager.partners[currentLeftPartner.partnerID].transform.position = partnerManager.partnerSpawnLocations[currentLeftPartner.partnerID].position;
        partnerManager.partners[currentLeftPartner.partnerID].SetActive(true);
    }

    public void ELeftSkill()
    {
        partnerManager.AssignESkillSlot(currentLeftPartner.partnerID);
        partnerManager.allPartners[currentLeftPartner.partnerID].selected = true;
        partnerManager.partners[currentLeftPartner.partnerID].transform.position = partnerManager.partnerSpawnLocations[currentLeftPartner.partnerID].position;
        partnerManager.partners[currentLeftPartner.partnerID].SetActive(true);
    }



}
