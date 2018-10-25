using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour {

    public float HP = 2;
    public float HPLeft;
    public CharacterController2D controller;
    [HideInInspector]public SpriteRenderer m_SpriteRender;
    public GameManager gameManager;


    void Awake () {
        m_SpriteRender = GetComponent<SpriteRenderer>();
        HPLeft = HP;
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
                StartCoroutine(BlinkSprite());
                StartCoroutine(DamageState());
            }
        }
    }


    IEnumerator DamageState()
    {
        controller.m_Damaged = true;
        TakeDamage(1);
        yield return new WaitForSeconds(1f);
        controller.m_Damaged = false;
        controller.m_Immune = false;
        
    }

    
    IEnumerator BlinkSprite()
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
    }

    public void TakeDamage(float damage)
    {
        if(!controller.m_Immune)
        {
            
            HPLeft -= damage;
            controller.m_Immune = true;
        }
        if(HPLeft == 0)
        {
            HPLeft = HP;
            gameManager.GameOver(this.transform);
        }
    }

    

    
}
