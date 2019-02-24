using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardBossHealth : BossHealth {
	public override void TakeDamage(float damage)
    {
        health -= damage;
        float currentHp = health / maxHealth;
        healthBar.fillAmount = currentHp;
        if(health <= 50 && !spikes[0].activeSelf)
        {
            StartCoroutine(TriggerSecondPhase());
        }

        if (health <= 0)
        {

            GetComponent<BossWorld2>().EndBattle();
			gameObject.SetActive(false);
            //health = maxHealth;
        }
    }
}
