using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : MonoBehaviour {

    private SoundEffectManager soundEffectManager;
    public List<Partner> allPartners;
    public List<GameObject> partners;
    public List<Transform> partnerSpawnLocations;
    public delegate void Skill();
    public Skill JSkill = null;
    public Skill KSkill = null;
    public Skill ESkill = null;

    public GameObject player;
    private CharacterController2D characterController;

    public SpriteMask spriteMask;
    public GameObject magicPlatform;
    public GameObject FireBall;

    //=====CoolDowns============
    public bool fireBallOnCoolDown;
    public float fireBallCoolDown;

    void Start()
    {
        characterController = player.GetComponent<CharacterController2D>();
        soundEffectManager = FindObjectOfType<SoundEffectManager>();
    }

    public void AssignJSkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref JSkill);
        
    }

    public void AssignKSkillSlot(int partnerCode)
    {
        AssignSkillToSlot(partnerCode, ref KSkill);
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
                skill = FireBallShot;
                break;

            case 1:
                skill = Dash;
                break;

            case 2:
                skill = CreateMagicPlatform;
                break;

            case 7:
                skill = NoSkill;
                break;
           
        }
    }



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

    public void LightPartner()
    {
        Debug.Log("Light");
        spriteMask.LightUp();

    }

    public void Dash()
    {
        characterController.Dash();
    }

    public void Hookshot()
    {
        Debug.Log("ArrowRope");
    }

    public void Shield()
    {
        Debug.Log("Shield");
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

    public void NoSkill() {}

    IEnumerator FireCoolDown(float coolDown)
    {
        
        yield return new WaitForSeconds(coolDown);
        fireBallOnCoolDown = false;
    }

    public void DeselectPartner(int partnerCode)
    {
        
    }
   
}
