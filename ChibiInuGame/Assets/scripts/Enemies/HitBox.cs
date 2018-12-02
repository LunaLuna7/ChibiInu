using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    //==========================================================
    //If player gets in contact with hit box the player gets hit
    //==========================================================

    public GameObject EnemyToKill;
    public float health;
    private SoundEffectManager soundEffectManager;
    private float timeBeforeDamageAgain = .1f;   //delay to prevent multi hits in trigger enter frames
    private float timetrack;
    [HideInInspector] public SpriteRenderer m_SpriteRender;
    [HideInInspector] public StateController stateController;



    public void Awake()
    {
        soundEffectManager = GameObject.FindGameObjectWithTag("SoundEffect").GetComponent<SoundEffectManager>();
        m_SpriteRender = GetComponentInParent<SpriteRenderer>();
        stateController = GetComponentInParent<StateController>();
        health = stateController.enemyStats.HP;
        timetrack = timeBeforeDamageAgain + Time.time;
    }

    //=========================================================
    //A enemy can be damaged on hitbox if its hit by a FireBall
    //=========================================================

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            if (timetrack <= Time.time)
            {
                timetrack = timeBeforeDamageAgain + Time.time;
                health--;
                if(this.enabled)
                    StartCoroutine(BlinkSprite());
            }

            if (health == 0)
            {
                soundEffectManager.Play("EnemyDeath");
                EnemyToKill.SetActive(false);
                health = stateController.enemyStats.HP;
            }
           
        }
    }

    //=========================================
    //Makes the Sprite of the Game Object blink
    //=========================================
    IEnumerator BlinkSprite()
    {
        for (int i = 0; i < 6; ++i)
        {
            yield return new WaitForSeconds(.05f);
            if (m_SpriteRender.enabled == true)
                m_SpriteRender.enabled = false;  
         
            else
                m_SpriteRender.enabled = true;   
        }
    }
}
