using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : MonoBehaviour {

    public List<Partner> allPartners;
    public List<GameObject> partners;
    public List<Transform> partnerSpawnLocations;
    public delegate void Skill();
    public Skill QSkill = null;
    public Skill WSkill = null;
    public Skill ESkill = null;

    public GameObject player;
    private CharacterController2D characterController;

    public SpriteMask spriteMask;
    public GameObject magicPlatform;

    void Start()
    {
        characterController = player.GetComponent<CharacterController2D>();
    }

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
                skill = CreateMagicPlatform;
                
                break;
            case 1:
                skill = LightPartner;
                break;

            case 2:
                characterController.m_AirJumps = 1;
                skill = DoubleJump;
                break;
           
        }
    }

    public void LightPartner()
    {
        spriteMask.LightUp();

    }

    public void CreateMagicPlatform()
    {
        if (characterController.m_FacingRight)
        {
            Instantiate(magicPlatform, player.transform.position + new Vector3(10, 0, 0), player.transform.rotation);
        }
        else
        {
            Instantiate(magicPlatform, player.transform.position + new Vector3(-10, 0, 0), player.transform.rotation);
        }
    }

    public void DoubleJump()
    {
        characterController.m_AirJumps = 1;
    }
}
