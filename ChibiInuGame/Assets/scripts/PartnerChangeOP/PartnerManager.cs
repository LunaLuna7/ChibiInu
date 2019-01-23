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
    public List<Partner> partners;
    public ScenePartnerHolder scenePartnerHolder;
    public Dictionary<SkillSlot, Partner> activePartner = new Dictionary<SkillSlot, Partner>();

    private CharacterController2D characterController;


    delegate void skillDelegate();
    skillDelegate firstSkill;
    skillDelegate secondSkill;

    
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
        foreach(Partner p in partners) 
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
                partners[pi.index].J = true;
            }
            else
            {
                AssignKSkillSlot(pi.index);
                partners[pi.index].K = true;
            }
            partners[pi.index].selected = true;
            //spawn the character
            partners[pi.index].transform.position = partnerSpawnLocations[pi.index].position;
            partners[pi.index].SetActive(true);
        }
    }
    private bool IsActive(int partnerId)
    {
        return partners[partnerId].inUse;
    }

 

    public void SummonPartner(SkillSlot skill, Partner partner)
    {   
         if (activePartner.ContainsKey(skill))
            activePartner[skill] = partner;
            
         else
            activePartner[skill] = partner;

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

   

    public void UnSummonPartner(Partner partner)
    {
        foreach(SkillSlot slot in activePartner.Keys)
        {
            if (activePartner[slot] == partner)
            {
                if (slot == SkillSlot.FirstSlot)
                    firstSkill = null;
                else
                    secondSkill = null;
                activePartner.Remove(slot);
            }

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            firstSkill();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            secondSkill();
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
