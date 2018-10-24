using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSlimeBossHealth : MonoBehaviour {

    [HideInInspector]public StateController stateController;
    [HideInInspector]public float health;

    private void Start()
    {
        stateController = GetComponent<StateController>();
        health = stateController.enemyStats.HP;
    }

    public void TakeDamage(float damage)
    { 
        health -= damage;
        if (health == 0){
           //die
        }
    }
}
