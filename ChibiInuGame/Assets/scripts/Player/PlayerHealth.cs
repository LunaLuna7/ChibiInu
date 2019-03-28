using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour {

    public float HP = 2;
    public float HPLeft;
    public CharacterController2D controller;
    public SpriteRenderer m_SpriteRender;
    public GameManager gameManager;
    public GameObject HealthUI;
    public Sprite fullHearth;
    public Sprite halfHearth;
    public Sprite emptyHearth;
    public Animator anim;
    private bool spikeCanHit;

    private Image playerHealth;

    void Awake () {
        spikeCanHit = true;
        m_SpriteRender = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        HPLeft = HP;
        playerHealth = HealthUI.GetComponent<Image>();
        playerHealth.sprite = fullHearth;
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
                //StartCoroutine(BlinkSprite());
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
        //controller.m_Damaged = true;
            TakeDamage(1);
        
        yield return new WaitForSeconds(1f);
        //controller.m_Damaged = false;
        if(!controller.m_OnShield)
            controller.m_Immune = false;
        
    }

    public IEnumerator DamageSpikeState()
    {
        if (spikeCanHit)
        {

            spikeCanHit = false;
            //controller.m_Damaged = true;
            HPLeft -= 1;
            controller.m_Immune = true;
            StartCoroutine(BlinkSprite());

            if (HPLeft == 1)
                playerHealth.sprite = halfHearth;

            else if (HPLeft == 2)
                playerHealth.sprite = fullHearth;

            if (HPLeft <= 0)
            {
                anim.Play("ShibDead");
                controller.m_Paralyzed = true;
                playerHealth.sprite = emptyHearth;
                gameManager.GameOver(this.transform);

                StartCoroutine(DelayHearth());
            }
        }

        yield return new WaitForSeconds(1f);
        //controller.m_Damaged = false;
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

    public void TakeDamage(float damage)
    {
        if(!controller.m_Immune)
        {
            
            HPLeft -= damage;
            controller.m_Immune = true;
            StartCoroutine(BlinkSprite());

            if (HPLeft == 1)
                playerHealth.sprite = halfHearth;

            else if (HPLeft == 2)
                playerHealth.sprite = fullHearth;

            if (HPLeft <= 0)
            {
                anim.Play("ShibaDead");
                controller.m_Paralyzed = true;
                playerHealth.sprite = emptyHearth;
                gameManager.GameOver(this.transform);

                StartCoroutine(DelayHearth());
            }
        }   
    }

    public void HealDamage()
    {
        HPLeft = HP;
        playerHealth.sprite = fullHearth;
    }


    public void ResetPlayer()
    {
        HPLeft = HP;
        playerHealth.sprite = fullHearth;
    }

    IEnumerator DelayHearth()
    {
        yield return new WaitForSeconds(1f);
        playerHealth.sprite = fullHearth;
    }

   
}
