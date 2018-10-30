using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtBox : MonoBehaviour {

    //public BoxCollider2D collider;
    public GameObject EnemyToKill;
    [HideInInspector]public StateController stateController;
    public float health;
    [HideInInspector]public SpriteRenderer m_SpriteRender;

    private float timeBeforeDamageAgain = .1f; //delay to prevent multi hits in trigger enter frames
    private float timetrack;

    public void Awake()
    {
        m_SpriteRender = GetComponentInParent<SpriteRenderer>();
        stateController = GetComponentInParent<StateController>();
        health = stateController.enemyStats.HP;
        timetrack = timeBeforeDamageAgain + Time.time;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if ((collision.gameObject.CompareTag("Player" ) && this.gameObject.transform.position.y - collision.gameObject.transform.position.y < 1)
            || collision.gameObject.CompareTag("FireBall"))//Player above
        {
            if (timetrack <= Time.time)
            {
                timetrack = timeBeforeDamageAgain + Time.time;
                health--;
                
                StartCoroutine(BlinkSprite());
            }

            if (health == 0)
            {
                EnemyToKill.SetActive(false);
                health = stateController.enemyStats.HP;
                //stateController.killed = true;
                //Destroy(EnemyToKill);
            }
        }
    }
    IEnumerator BlinkSprite()
    {
        for (int i = 0; i < 6; ++i)
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




}
