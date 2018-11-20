using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIBook))]
public class SkillSummonUI : MonoBehaviour {

    public UIBook uiBook;
    public CurrentTeamSlotUI teamSlot;

    public void SummonPartner(string skill)
    {
        AssignSkill(skill);
        uiBook.partnerManager.allPartners[uiBook.currentPartner.partnerID].selected = true;
        uiBook.partnerManager.partners[uiBook.currentPartner.partnerID].transform.position = uiBook.partnerManager.partnerSpawnLocations[uiBook.currentPartner.partnerID].position;
        uiBook.partnerManager.partners[uiBook.currentPartner.partnerID].SetActive(true);
    }

    public void AssignSkill(string skill)
    {
        switch(skill){ 

            case "J":
                ResetJPartner();
                uiBook.partnerManager.AssignJSkillSlot(uiBook.currentPartner.partnerID);
                uiBook.currentPartner.J = true;
                teamSlot.UpdatePartners();
                teamSlot.UpdateImages();
                break;
            case "K":
                ResetKPartner();
                uiBook.partnerManager.AssignKSkillSlot(uiBook.currentPartner.partnerID);
                uiBook.currentPartner.K = true;
                teamSlot.UpdatePartners();
                teamSlot.UpdateImages();
                break;

            /*case "L":
                ResetEPartner();
                uiBook.partnerManager.AssignESkillSlot(uiBook.currentPartner.partnerID);
                uiBook.currentPartner.E = true;
                teamSlot.UpdatePartners();
                teamSlot.UpdateImages();
                break;*/
        }
    }

    public void ResetJPartner()
    {
        for(int i = 0; i < uiBook.partnerManager.allPartners.Count; ++i)
        {
            uiBook.partnerManager.allPartners[i].J = false;
            uiBook.partnerManager.allPartners[i].selected = false;

        }
        uiBook.partnerManager.AssignJSkillSlot(7); //7 is empty skill
    }

    public void ResetKPartner()
    {
        for (int i = 0; i < uiBook.partnerManager.allPartners.Count; ++i)
        {
            uiBook.partnerManager.allPartners[i].K = false;
            uiBook.partnerManager.allPartners[i].selected = false;
        }
        uiBook.partnerManager.AssignKSkillSlot(7);
    }

    public void CallBackPartner()
    {
        if (uiBook.currentPartner.J == true)
            ResetJPartner();

        else if (uiBook.currentPartner.K)
            ResetKPartner();
    }

    /*
    public void ResetEPartner()
    {
        for (int i = 0; i < uiBook.partnerManager.allPartners.Count; ++i)
        {
            uiBook.partnerManager.allPartners[i].E = false;
        }
        uiBook.partnerManager.AssignESkillSlot(7);
    }*/
    /*
      public enum Skills {
          Qskill,
          Wskill,
          Eskill
      };
      */

}
