using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour {

    public GameObject DeadStateEnemy;
    [HideInInspector] public SpriteRenderer m_SpriteRender;

    private float timeBeforeDamageAgain = .1f; //delay to prevent multi hits in trigger enter frames
    private float timetrack;

    public void Awake()
    {
        m_SpriteRender = GetComponentInParent<SpriteRenderer>();
        timetrack = timeBeforeDamageAgain + Time.time;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("FireBall"))//Player above
        {
            if (timetrack <= Time.time)
            {
                timetrack = timeBeforeDamageAgain + Time.time;
                Bosshealth.health--;
                StartCoroutine(BlinkSprite());
            }
        }*/
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
