using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New monster", menuName = "New Monster")]
[System.Serializable]
public class MonsterScriptableObject : ScriptableObject {
    public string monsterName = "New monster";
    [Header("Stats")]
    public int health = 0;
    public int rangeOrMaxDistance = 0;
    public int minDamage = 0;
    public int maxDamage = 0;
    public int attackSpeed = 2; 
    public int speed = 32;

    [Header("Ranged")]
    public GameObject projectile;
    public float attackDelay = 0;
    public bool delayedAttacks = false;
    public bool ranged = false;

    [Header("Rewards")]
    public int coinsReward;
    public int expReward;

    [Header("Boss settings")]
    public bool spawnPortalOnDeath;
    public bool multiObjectEnemy;
    public bool bossObject;
}
