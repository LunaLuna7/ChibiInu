using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : MonoBehaviour {

    public List<Partner> allPartners;
    public delegate void Skill();
    public Skill QSkill = null;
    public Skill WSkill = null;
    public Skill ESkill = null;

    public void AssignQSkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref QSkill);
    }

    public void AssignWSkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref WSkill);
    }

    public void AssignESkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref ESkill);
    }

    void AssignSkillToSlot(int partnerCode, ref Skill skill)
    {
        switch (partnerCode)
        {
            case 0:
                skill = TripleJump;
                
                break;
            case 1:
                skill = FourJump;
                break;
           
        }
    }

    void TripleJump()
    {
        Debug.Log("3 jumps");
    }

    void FourJump()
    {
        Debug.Log("4 jumps");
    }
}
