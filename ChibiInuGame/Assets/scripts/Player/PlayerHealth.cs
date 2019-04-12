using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour {

    public int HP = 2;
    public int HPLeft;
    public CharacterController2D controller;
    public SpriteRenderer m_SpriteRender;
    public GameManager gameManager;
    public GameObject HealthUI;
    public List<Sprite> hearths;
    public Animator anim;
    private bool spikeCanHit;

    private Image playerHealth;

    void Awake () {
        spikeCanHit = true;
        m_SpriteRender = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        HPLeft = HP;
        playerHealth = HealthUI.GetComponent<Image>();
        playerHealth.sprite = hearths[HPLeft];
    }
	
    
    void OnTriggerStay2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "hurtBox")
        {
            controller.m_RigidBody2D.velocity = new Vector2(controller.m_RigidBody2D.velocity.x, 25);
        }
        if (collide.gameObject.tag == "hitBox")
        {
            if (!controller.m_Immune)
            {
                StartCoroutine(DamageState());
            }
        }
        if(collide.gameObject.tag == "GSlimeHitBox")
        {
            TakeDamage(2);
            controller.m_Immune = false;
        }
    }


    public IEnumerator DamageState()
    {
        TakeDamage(1);
      
        yield return new WaitForSeconds(1f);
        if(!controller.m_OnShield)
            controller.m_Immune = false;
        
    }

    public IEnumerator DamageSpikeState()
    {
        if (spikeCanHit)
        {

            spikeCanHit = false;
            HPLeft -= 1;
            anim.Play("ShibDead");
            controller.m_Immune = true;
            StartCoroutine(BlinkSprite());

            
            
            playerHealth.sprite = hearths[HPLeft];
            if (HPLeft <= 0)
            {
                controller.m_Paralyzed = true;
                gameManager.GameOver(this.transform);

                StartCoroutine(DelayHearth());
            }
        }

        yield return new WaitForSeconds(1f);
        spikeCanHit = true;
        if(!controller.m_OnShield)
            controller.m_Immune = false;

    }


    public IEnumerator BlinkSprite()
    {
        for(int i = 0; i < 8; ++i)
        {
            yield return new WaitForSeconds(.05f);
            if (m_SpriteRender.enabled == true)
            {
                m_SpriteRender.enabled = false;
            }
            else
            {
                m_SpriteRender.enabled = true;
            }
        }
        m_SpriteRender.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        if(!controller.m_Immune)
        {
            
            HPLeft -= damage;
            anim.Play("ShibDead");
            controller.m_Immune = true;
            StartCoroutine(BlinkSprite());

           
            playerHealth.sprite = hearths[HPLeft];
            if (HPLeft <= 0)
            {
                controller.m_Paralyzed = true;
                gameManager.GameOver(this.transform);
                StartCoroutine(DelayHearth());
            }
        }   
    }

    public void HealDamage()
    {
        HPLeft = HP;
        playerHealth.sprite = hearths[HP];
    }


    public void ResetPlayer()
    {
        HPLeft = HP;
        playerHealth.sprite = hearths[HP];
    }

    IEnumerator DelayHearth()
    {
        yield return new WaitForSeconds(1f);
        playerHealth.sprite = hearths[HP];
    }

   
}
