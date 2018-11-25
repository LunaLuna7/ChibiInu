using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    [HideInInspector] public StateController stateController;
    public float health;
    private float maxHealth;
    public Image healthBar;

    private void Start()
    {
        stateController = GetComponent<StateController>();
        health = maxHealth = stateController.enemyStats.HP;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        float currentHp = health / maxHealth;
        healthBar.fillAmount = currentHp;
        if (health <= 0)
        {

            gameObject.SetActive(false);
            health = stateController.enemyStats.HP;
        }
    }
}
