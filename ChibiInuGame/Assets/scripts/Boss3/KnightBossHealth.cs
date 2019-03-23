using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBossHealth : BossHealth {
    public KnightBossPhaseManager phaseManager;
    public override void TakeDamage(float damage)
    {
        health -= damage;
        SoundEffectManager.instance.Play("SlimeDeath");
        float currentHp = health / maxHealth;
        healthBar.fillAmount = currentHp;

        if (health <= 0)
        {
            GetComponent<KnightBossManager>().EndBattle();
            gameObject.SetActive(false);
            health = maxHealth;
            
        }else if (currentHp <= 0.7f && phaseManager.GetPhase() == 1)
        {
            //phase 2
            phaseManager.EnterPhase(2);
        }else if (currentHp <= 0.3f && phaseManager.GetPhase() == 2)
        {
            //phase 3
            phaseManager.EnterPhase(3);
        }
        
    }

}
