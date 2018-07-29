using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour {

    public float health = 2;
    public LevelChanger levelChanger;
    public CharacterController2D controller;
    public SpriteRenderer m_SpriteRender;

    // Use this for initialization
    void Awake () {
        levelChanger = GameObject.FindGameObjectWithTag("LevelChanger").GetComponent<LevelChanger>();
        m_SpriteRender = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
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
                m_SpriteRender.enabled = false;  //make changes
            }
            else
            {
                m_SpriteRender.enabled = true;   //make changes
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if(!controller.m_Immune)
        {
            
            health -= damage;
            controller.m_Immune = true;
        }
        if(health == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        levelChanger.FadeToSameLevel();
    }

    
}
