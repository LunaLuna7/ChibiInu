using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossAttack{

    [Header("Attack Data")]
    public string ID;
    public GameObject attack; //Pysical attack object
    public float spawnWait;       //wait between each repetition of attack
    public float coolDown;      //how much time to wait for next attack
    public float warningWait;    //wait between string and actual attack
    public string attackWarning; //string that boss speaks
    public bool isNormal;   //check if special attack to give more wait time and warning
    public int repetitions; //how many times object is spawn
    public Transform location; //Place where it spawns
}
