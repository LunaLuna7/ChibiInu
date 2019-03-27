using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    //==========================================================
    //If player gets in contact with hit box the player gets hit
    //==========================================================

    public GameObject EnemyToKill;
    //public float health;
    private float timeBeforeDamageAgain = .1f;   //delay to prevent multi hits in trigger enter frames
    private float timetrack;
    [HideInInspector] public SpriteRenderer m_SpriteRender;
    [HideInInspector] public StateController stateController;
    public GameObject deadParticle;


    public void Awake()
    {
        m_SpriteRender = GetComponentInParent<SpriteRenderer>();
        stateController = GetComponentInParent<StateController>();
        //health = stateController.enemyStats.HP;
        timetrack = timeBeforeDamageAgain + Time.time;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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
                stateController.health--;
                if (this.enabled && stateController.health != 0)
                {
                    StartCoroutine(BlinkSprite());
                }
            }

            if (stateController.health == 0)
            {
                StopAllCoroutines();
                SoundEffectManager.instance.Play("SlimeDeath");
                EnemyToKill.SetActive(false);
                Instantiate(particleDead, transform.position, Quaternion.identity);
                stateController.health = stateController.enemyStats.HP;
            }

        }
        /*
        else if (collision.gameObject.CompareTag("Spike"))
        {
            Rigidbody2D temp = gameObject.GetComponentInParent<Rigidbody2D>();
            if(temp!=null)
                temp.transform.parent.gameObject.SetActive(false);

        }*/
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
