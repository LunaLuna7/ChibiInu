using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillSlot{
    FirstSlot,
    SecondSlot
}

public class PartnerManager : MonoBehaviour {

    //================================================================================================
    //PartnerManager handles the assinging what skill to what Skill object depending on scriptable Obj
    //================================================================================================


    private SoundEffectManager soundEffectManager;
    public List<Partner> allPartners;
    public ScenePartnerHolder scenePartnerHolder;
    public Dictionary<SkillSlot, Partner> activePartner = new Dictionary<SkillSlot, Partner>();

    private CharacterController2D characterController;
  


    public SpriteMask spriteMask; //the darkness layer that clocks the level for ch4
    public GameObject FireBall; //The fireball prefab

    //=====CoolDowns============
    public bool fireBallOnCoolDown;
    public float fireBallCoolDown;

    void Start()
    {
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
        //initialize partners
        //reset for level
        foreach(Partner p in allPartners) 
        {
            p.J = false;
            p.K = false;
            p.selected = false;
        }
        //get the partner in use

        foreach (PartnerInfo pi in SaveManager.dataInUse.partners)
        {
            if(pi.skillSlot != "J" && pi.skillSlot != "K")
            {
                Debug.LogWarning("Unknown key stored in partner save data: " + pi.skillSlot + " should be J or K.");
                continue;
            }
            //assign different keys
            if(pi.skillSlot == "J")
            {
                AssignJSkillSlot(pi.index);
                allPartners[pi.index].J = true;
            }
            else
            {
                AssignKSkillSlot(pi.index);
                allPartners[pi.index].K = true;
            }
            allPartners[pi.index].selected = true;
            //spawn the character
            partners[pi.index].transform.position = partnerSpawnLocations[pi.index].position;
            partners[pi.index].SetActive(true);
        }
    }

    //calls assingSkillToSlot with JSkill as paramter so it calls such method in PlayerPartnerSkills.cs
    /*public void AssignJSkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref JSkill);
        
    }

    //calls assingSkillToSlot with KSkill as paramter so it calls such method in PlayerPartnerSkills.cs
    public void AssignKSkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref KSkill);
    }*/


    //PartnerCode is the ID of the scriptableObject partner and it matches the case number of switch. skill is wether it will be for J or K click
    //Ex: ID for wizard partner is 0, so when their ID is passed in partnerCode it will go to case 0 which assigns the FireBallShot method to either J or K
    /*void AssignSkillToSlot(int partnerCode, ref Skill skill)
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
    }*/


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
