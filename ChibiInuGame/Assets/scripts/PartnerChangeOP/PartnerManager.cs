﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillSlot{
    FirstSlot,
    SecondSlot
}

/// <summary>
/// Contains all the skills of each partner, Assign skills to player Input,
/// Keeps track of active partners as well as scene partner objects, loads/saves player prefs in regard to partners, and handles user Input
/// </summary>
public class PartnerManager : MonoBehaviour {

    public CharacterController2D characterController;
    private SoundEffectManager soundEffectManager;

    public List<PartnerInfo> partnersInfo;
    public List<Partner> partners;
    [HideInInspector]public ScenePartnerHolder scenePartnerHolder;
    public Dictionary<SkillSlot, Partner> activePartner = new Dictionary<SkillSlot, Partner>(); //Used to know which partner is active and make it easy to diselect


    //Skills used on player Input
    delegate void skillDelegate();
    skillDelegate firstSkill;
    skillDelegate secondSkill;

    [Space]
    [Header("Skills elements")]
    public SpriteMask spriteMask; //the darkness layer that clocks the level for ch4
    public GameObject FireBall; //The fireball prefab


    //=====CoolDowns============
    public bool fireBallOnCoolDown;
    public float fireBallCoolDown;

    SkillSlot temp;
    public bool secondPartnerSlotUnlock;

    private void Awake()
    {
        CreatePartners();
    }

    void Start()
    {
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
        scenePartnerHolder = GetComponent<ScenePartnerHolder>();
        //initialize partners
        //reset for level
        foreach(Partner p in partners) 
        {
            p.inUse = false;
        }
        activePartner.Clear();

        //TODO: update this code with new system
        //TODO: load the activePartner depending on load file depending on the Partner.cs bools
        //It should also load on scene load I believe, since it should keep track from scene to scene
        //load the partner in use, partner's index in the arrary need to be the same as their partner id
        foreach (ActivePartnerInfo pi in SaveManager.dataInUse.activePartners)
        {
            if(pi.skillSlot != SkillSlot.FirstSlot && pi.skillSlot != SkillSlot.SecondSlot)
            {
                Debug.LogWarning("Unknown key stored in partner save data: " + pi.skillSlot);
                continue;
            }
            else{
                //load active partner
                SummonPartner(pi.skillSlot, partners[pi.index]);
            }
            partners[pi.index].inUse = true;
            //spawn the character
            scenePartnerHolder.ChangePartnerImage(pi.skillSlot, partners[pi.index].partnerInfo.image);
        }
    }

    private bool IsActive(int partnerId)
    {
        return partners[partnerId].inUse;
    }

    public void CreatePartners()
    {
        partners.Clear();
        foreach(PartnerInfo p in partnersInfo)
        {
            Partner partner = new Partner(false, false);
            partner.partnerInfo = p;
            partners.Add(partner);
        }
    }
 
    //Assing the skill delegate to the respective partner and updates the activePartners dictionary
    public void SummonPartner(SkillSlot skill, Partner partner)
    {
        if (activePartner.ContainsKey(skill)) //if another partner is currrently occupying the skillSlot, unsummon such
        {
            activePartner[skill].inUse = false;
        }

         if (activePartner.ContainsKey(skill))
            activePartner[skill] = partner;
            
         else
            activePartner[skill] = partner;
        //change partner image in scene
        scenePartnerHolder.ChangePartnerImage(skill, partner.partnerInfo.image);
        switch (partner.partnerInfo.partnerId)
        {
            case 0:
                
                if(skill == SkillSlot.FirstSlot) firstSkill = FireBallShot;
                else secondSkill = FireBallShot;
                break;

            case 1:
                if (skill == SkillSlot.FirstSlot) firstSkill = Dash;
                else secondSkill = Dash;
                break;

            case 2:
                if (skill == SkillSlot.FirstSlot) firstSkill = Shield;
                else secondSkill = Shield;
                break;

            case 3:
                if (skill == SkillSlot.FirstSlot) firstSkill = LightPartner;
                else secondSkill = LightPartner;
                break;
        }

    }

    public void LimitPlayerJump(bool b)
    {
        if (TripleJumpPartnerCapacity())
            characterController.m_AirJumps = 2;
        else
            characterController.m_AirJumps = 1;
    }

    public void PartnerInUse(Partner partner)
    {
        partner.inUse = true;
    }
   
    //Updates the dict activePartner and makes the partner's in use = false 
    public void UnSummonPartner(Partner partner)
    {
        partner.inUse = false;
        bool foundPartner = false;
        foreach(SkillSlot slot in activePartner.Keys)
        {
            if (activePartner[slot] == partner)
            {
                if (slot == SkillSlot.FirstSlot)
                    firstSkill = null;
                else
                    secondSkill = null;
                temp = slot;
                foundPartner = true;
                partner.inUse = false;
            }

        }
        if (foundPartner)
        {
            activePartner.Remove(temp);
            //update image in scene
            scenePartnerHolder.ChangePartnerImage(temp, null);

            
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            firstSkill();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            secondSkill();
        }
    }

    //Takes the partner ID and unlcoks the partner. Partner's IDs: Wizard:0, Bard: 1, Knight: 2, Priest: 3
    public void UnlockPartner(int Id)
    {
        foreach(Partner p in partners)
        {
            if(p.partnerInfo.partnerId == Id)
            {
                p.unlocked = true;
            }
        }
    }

    //returns wether the player is allowed to have 3 jumps depending on in used partners
    public bool TripleJumpPartnerCapacity()
    {
        if (secondPartnerSlotUnlock) //If pass ch2 then player can 3 jump if they only have one partner in use
        {
            int counter = 0;
            foreach (Partner p in partners)
            {
                if (p.inUse)
                {
                    counter++;
                    if (counter >= 2)
                        return false;
                }
            }
            return true;
        }
        else
        {
            foreach(Partner p in partners)
            {
                if (p.inUse)
                {

                    Debug.Log("Triple");
                    return false;
                }
            }
            return true;
        }
    }

    //==============================================================================
    //Methods that create or call each partner skills
    //==============================================================================

    
    //Creates a Fireball
    public void FireBallShot()
    {
        if (!fireBallOnCoolDown)
        {
            soundEffectManager.Play("FireBall");
            if (characterController.m_FacingRight)
                Instantiate(FireBall, characterController.fireSpawn.position, Quaternion.identity);
            else
            {
                GameObject a = (GameObject)Instantiate(FireBall, characterController.fireSpawn.position, Quaternion.identity);
                Transform trans = a.GetComponent<Transform>();
                trans.localScale = trans.localScale * -1;
            }
            fireBallOnCoolDown = true;
            StartCoroutine(FireCoolDown(fireBallCoolDown));       
        }
    }

    //Creates a Light
    public void LightPartner()
    {
        Debug.Log("Light");
        spriteMask.LightUp();
    }

    //Acces CharacterController Dash
    public void Dash()
    {
        characterController.Dash();
    }

    //Partners shield(yet to implement)
    public void Shield()
    {
        Debug.Log("Shield");
    }

    //CoolDown time for the fireball
    IEnumerator FireCoolDown(float coolDown)
    {       
        yield return new WaitForSeconds(coolDown);
        fireBallOnCoolDown = false;
    }
   
}