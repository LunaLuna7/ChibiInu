using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossAttack{

    [Header("Attack Data")]
    public GameObject attack;
    public int cooldown;
    public float spawnWait;
    public float warningWait;    
    public string attackWarning;
    public bool isNormal;
}
