using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {
    
    public float health;
    public float maxHealth;
    public float intervalSpikesTime;
    public Image healthBar;

    public ControlCameraShake cameraShake;

    [Header("for Phase 2 Stage")]
    //Left, Up, right, and down
    public List<GameObject> spikes;

    protected void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = maxHealth;
    }

    public virtual void TakeDamage(float damage)
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

            gameObject.SetActive(false);
            //health = maxHealth;
        }
    }

    protected IEnumerator TriggerSecondPhase()
    {
        foreach(GameObject spike in spikes)
        {
            cameraShake.shakeElapsedTime = cameraShake.shakeDuration;
            cameraShake.spikeTrigger = true;
            spike.SetActive(true);
            yield return new WaitForSeconds(intervalSpikesTime);
        }
    }

}
