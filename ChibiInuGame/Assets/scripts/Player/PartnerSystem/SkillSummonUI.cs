using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIBook))]
public class SkillSummonUI : MonoBehaviour {
    /// <summary>
    /// SkillSummonUI handles the assinging skills to specific key codes when the player presses a UI button depending on the string given as a parameter when
    /// succh button is pressed. The string parameter can be specified in the Unity console where the Button is located adn when it calls AssignSKill method onClick
    /// </summary>

    public UIBook uiBook;
    public CurrentTeamSlotUI teamSlot;

    //If partner selected through book, set such partner game object active in the scene
    public void SummonPartner(string skill)
    {
        AssignSkill(skill);
        uiBook.partnerManager.partners[uiBook.currentPartner.partnerID].selected = true;
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

            
        }
    }

    //unasing J skill by assinging it to 7(which is in PartnerManager.cs  AssingSkillToSlot method in the switch loop)
    public void ResetJPartner()
    {
        for(int i = 0; i < uiBook.partnerManager.partners.Count; ++i)
        {
            uiBook.partnerManager.partners[i].J = false;
            uiBook.partnerManager.partners[i].selected = false;

        }
        uiBook.partnerManager.AssignJSkillSlot(7); //7 is empty skill
    }

    //unasing K skill by assinging it to 7(which is in PartnerManager.cs  AssingSkillToSlot method in the switch loop)
    public void ResetKPartner()
    {
        for (int i = 0; i < uiBook.partnerManager.partners.Count; ++i)
        {
            uiBook.partnerManager.partners[i].K = false;
            uiBook.partnerManager.partners[i].selected = false;
        }
        uiBook.partnerManager.AssignKSkillSlot(7);
    }

    //Calls reset K or J depending on currentPartner
    public void CallBackPartner()
    {
        if (uiBook.currentPartner.J == true)
            ResetJPartner();

        else if (uiBook.currentPartner.K)
            ResetKPartner();
    }

   
    /*
      public enum Skills {
          Qskill,
          Wskill,
          Eskill
      };
      */

}
