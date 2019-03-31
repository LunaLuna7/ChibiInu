using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanBossHealth : BossHealth {

    public GameObject fakePartner;
	public override void TakeDamage(float damage)
    {
        health -= damage;
        SoundEffectManager.instance.Play("SlimeDeath");
        float currentHp = health / maxHealth;
        healthBar.fillAmount = currentHp;
        if (health <= 0)
        {
            fakePartner.SetActive(false);
            GetComponent<SatanBossManager>().EndBattle();
            gameObject.SetActive(false);
            SoundEffectManager.instance.Stop("SatanBattle");
            //health = maxHealth;
        }
    }

    public void Reset()
    {
        health = maxHealth;
        healthBar.fillAmount = 1;
    }
	
}
