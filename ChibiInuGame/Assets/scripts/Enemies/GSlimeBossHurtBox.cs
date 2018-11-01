using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSlimeBossHurtBox : MonoBehaviour {

    //public BoxCollider2D collider;
    public GameObject DeadStateObject;
    [HideInInspector] public StateController stateController;
    [HideInInspector] public SpriteRenderer m_SpriteRender;
    [HideInInspector] public GSlimeBossHealth bossHealth;

    private float timeBeforeDamageAgain = .1f; //delay to prevent multi hits in trigger enter frames
    private float timetrack;

    public void Awake()
    {
        m_SpriteRender = GetComponentInParent<SpriteRenderer>();
        stateController = GetComponentInParent<StateController>();
        bossHealth = GetComponentInParent<GSlimeBossHealth>();
        timetrack = timeBeforeDamageAgain + Time.time;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("FireBall"))
        {
            if (timetrack <= Time.time)
            {
                timetrack = timeBeforeDamageAgain + Time.time;
                bossHealth.TakeDamage(1);
                StartCoroutine(BlinkSprite());
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
                m_SpriteRender.enabled = false;  
            }
            else
            {
                m_SpriteRender.enabled = true;
            }
        }
    }



}
