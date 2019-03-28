using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtBox : MonoBehaviour {

    //==========================================================
    //If player gets in contact with hit box the enemy gets hit
    //==========================================================

    public GameObject EnemyToKill;
    public bool immune;
    public bool immuneToFire;
    //public float health;
    private float timeBeforeDamageAgain = .1f; //delay to prevent multi hits in trigger enter frames
    private float timetrack;
    [HideInInspector]public StateController stateController;
    [HideInInspector]public SpriteRenderer m_SpriteRender;
    public GameObject particleDead;

    public void Awake()
    {
        
        m_SpriteRender = GetComponentInParent<SpriteRenderer>();
        stateController = GetComponentInParent<StateController>();
        //health = stateController.enemyStats.HP;
        timetrack = timeBeforeDamageAgain + Time.time;

    }

    //==============================================================
    //A enemy can be damaged if its hit by a FireBall or by a player
    //==============================================================

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!immune && (collision.gameObject.CompareTag("Player" ) && this.gameObject.transform.position.y - collision.gameObject.transform.position.y < 1)
            || (collision.gameObject.CompareTag("FireBall") && !immuneToFire))//Checks if Player is truly above the hitbox to make sure they die on contact only if player jumps on them
        {
            if (timetrack <= Time.time && EnemyToKill.activeSelf)
            {
                timetrack = timeBeforeDamageAgain + Time.time;
                stateController.health--;
                StartCoroutine(BlinkSprite());
            }

            if (stateController.health <= 0)
            {
                StopAllCoroutines();
                SoundEffectManager.instance.Play("SlimeDeath");
                
                stateController.killed = true;
                stateController.health = stateController.enemyStats.HP;
                EnemyToKill.SetActive(false);
                Instantiate(particleDead, transform.position, Quaternion.identity);
                //StartCoroutine(DelayInactive());
            }
        }
    }

    //=========================================
    //Makes the Sprite of the Game Object blink
    //=========================================

    IEnumerator BlinkSprite()
    {
        //stateController.tempImmune = true;
        for (int i = 0; i < 6; ++i)
        {
            yield return new WaitForSeconds(.05f);
            if (m_SpriteRender.enabled == true)
                m_SpriteRender.enabled = false;
            
            else
                m_SpriteRender.enabled = true;
        }
        //stateController.tempImmune = false;
    }

    private void OnEnable()
    {
        stateController.killed = false;
        m_SpriteRender.enabled = true;
    }


}
