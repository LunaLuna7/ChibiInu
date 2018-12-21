using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : MonoBehaviour {

    //================================================================================================
    //PartnerManager handles the assinging what skill to what Skill object depending on scriptable Obj
    //================================================================================================


    private SoundEffectManager soundEffectManager;
    public List<Partner> allPartners; //Partner Scriptable Objects with each partner data
    public List<GameObject> partners; //Scene game objects that when selected follow the player around. They can be found in PartnersSystem GameObject in the scene always right bellow Player Game Obj
    public List<Transform> partnerSpawnLocations; //the 3 locations in whihc partners cna spawn and follow the player

    //Skills objects are used to determine which skill will be call when clicking J or K keybord key depending on selected partner
    //in PlayerPartnerSkills.cs
    public delegate void Skill();   
    public Skill JSkill = null;
    public Skill KSkill = null;

    public GameObject player;
    private CharacterController2D characterController;

    public SpriteMask spriteMask; //the darkness layer that clocks the level for ch4
    public GameObject FireBall; //The fireball prefab

    //=====CoolDowns============
    public bool fireBallOnCoolDown;
    public float fireBallCoolDown;

    void Start()
    {
        characterController = player.GetComponent<CharacterController2D>();
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }

    //calls assingSkillToSlot with JSkill as paramter so it calls such method in PlayerPartnerSkills.cs
    public void AssignJSkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref JSkill);
        
    }

    //calls assingSkillToSlot with KSkill as paramter so it calls such method in PlayerPartnerSkills.cs
    public void AssignKSkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref KSkill);
    }


    //PartnerCode is the ID of the scriptableObject partner and it matches the case number of switch. skill is wether it will be for J or K click
    //Ex: ID for wizard partner is 0, so when their ID is passed in partnerCode it will go to case 0 which assigns the FireBallShot method to either J or K
    void AssignSkillToSlot(int partnerCode, ref Skill skill)
    {
        switch (partnerCode)
        {
            case 0:
                skill = FireBallShot;
                break;

            case 1:
                skill = Dash;
                break;

            case 7:
                skill = NoSkill;
                break;   
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

    public void NoSkill() {}

    //CoolDown time for the fireball
    IEnumerator FireCoolDown(float coolDown)
    {       
        yield return new WaitForSeconds(coolDown);
        fireBallOnCoolDown = false;
    }

    public void DeselectPartner(int partnerCode)
    {
        
    }
   
}
