using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject {

    public float HP;
    public float moveSpeed;
    public float lookRange;
    public float attackRate;
    public float jumpPower;
    public float jumpRate;
    public float attackDamage;
}
