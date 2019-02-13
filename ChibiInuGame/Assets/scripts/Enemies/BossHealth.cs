using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {
    
    public float health;
    public float maxHealth;
    public float intervalSpikesTime;
    public Image healthBar;
    public Transform cameraTransform;
    public float shakeDuration;
    public float shakeAmount;

    [Header("for Phase 2 Stage")]
    //Left, Up, right, and down
    public List<GameObject> spikes;

    private void Start()
    {
        
        health = maxHealth;
    }

    public void TakeDamage(float damage)
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

    IEnumerator TriggerSecondPhase()
    {
        ShakeCamera(shakeDuration);
        foreach(GameObject spike in spikes)
        {
            spike.SetActive(true);
            yield return new WaitForSeconds(intervalSpikesTime);
        }
    }

    public void ShakeCamera(float duration)
    {
        Vector3 originalPos = cameraTransform.position;
        while(duration > 0)
        {
            Debug.Log("SHake");
            cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            duration -= Time.deltaTime;
        }
        
        cameraTransform.position = originalPos;
    }
}
