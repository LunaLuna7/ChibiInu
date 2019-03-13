using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardBossHealth : BossHealth {

    public int secondPhaseHealth;
    int countSpike = 0;
    public override void TakeDamage(float damage)
    {
        health -= damage;
        float currentHp = health / maxHealth;
        healthBar.fillAmount = currentHp;
        if(health <= secondPhaseHealth && !spikes[0].activeSelf)
        {
            StartCoroutine(TriggerSecondPhase());
        }
        GetComponent<BossWorld2>().IndicateToPushPlayer();

        if (health <= 0)
        {
            
            GetComponent<BossWorld2>().EndBattle();
			gameObject.SetActive(false);
            SoundEffectManager.instance.Stop("Boss");
            foreach (GameObject spike in spikes)
            {
                spike.SetActive(false);
                SpikeAttack temp = spike.GetComponent<SpikeAttack>();
                if (countSpike < 4)
                {
                    countSpike++;
                    temp.SetStartPosition();
                }
                temp.ResetPosition();
            }
            //health = maxHealth;
        }
    }
}
