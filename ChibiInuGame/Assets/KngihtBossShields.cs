using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KngihtBossShields : MonoBehaviour {

    public int shieldHealth = 3;
    public int currentShieldHealth;
    SpriteRenderer spriteRender;
    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        currentShieldHealth = shieldHealth;
    }


    // Use this for initialization
    void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            DamageShield();
            Destroy(collision.gameObject);
        }
    }


    void DamageShield()
    {
        currentShieldHealth--;
        if (currentShieldHealth < 1)
            gameObject.SetActive(false);
        else
            StartCoroutine(BlinkSprite());
    }


    public IEnumerator BlinkSprite()
    {
        for (int i = 0; i < 8; ++i)
        {
            yield return new WaitForSeconds(.05f);
            if (spriteRender.enabled == true)
            {
                spriteRender.enabled = false;
            }
            else
            {
                spriteRender.enabled = true;
            }
        }
        spriteRender.enabled = true;
    }
}
