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

            case "Q":
                ResetQPartner();
                uiBook.partnerManager.AssignQSkillSlot(uiBook.currentPartner.partnerID);
                uiBook.currentPartner.Q = true;
                teamSlot.UpdatePartners();
                teamSlot.UpdateImages();
                break;
            case "W":
                ResetWPartner();
                uiBook.partnerManager.AssignWSkillSlot(uiBook.currentPartner.partnerID);
                uiBook.currentPartner.W = true;
                teamSlot.UpdatePartners();
                teamSlot.UpdateImages();
                break;
            case "E":
                ResetEPartner();
                uiBook.partnerManager.AssignESkillSlot(uiBook.currentPartner.partnerID);
                uiBook.currentPartner.E = true;
                teamSlot.UpdatePartners();
                teamSlot.UpdateImages();
                break;
        }
    }

    public void ResetQPartner()
    {
        for(int i = 0; i < uiBook.partnerManager.allPartners.Count; ++i)
        {
            uiBook.partnerManager.allPartners[i].Q = false;
        }
        uiBook.partnerManager.AssignQSkillSlot(7); //7 is empty skill
    }

    public void ResetWPartner()
    {
        for (int i = 0; i < uiBook.partnerManager.allPartners.Count; ++i)
        {
            uiBook.partnerManager.allPartners[i].W = false;
        }
        uiBook.partnerManager.AssignWSkillSlot(7);
    }

    public void ResetEPartner()
    {
        for (int i = 0; i < uiBook.partnerManager.allPartners.Count; ++i)
        {
            uiBook.partnerManager.allPartners[i].E = false;
        }
        uiBook.partnerManager.AssignESkillSlot(7);
    }
    /*
      public enum Skills {
          Qskill,
          Wskill,
          Eskill
      };
      */

}
