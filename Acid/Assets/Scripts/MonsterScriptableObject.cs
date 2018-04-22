using UnityEngine;

[CreateAssetMenu(fileName = "New monster", menuName = "New Monster")]
[System.Serializable]
public class MonsterScriptableObject : ScriptableObject {
    public string monsterName = "New monster";
    public int health = 0;
    public int rangeOrMaxDistance = 0;
    public int minDamage = 0;
    public int maxDamage = 0;
    public int attackSpeed = 2; 
    public int speed = 32;
    public bool ranged = false;
    public bool spawnPortalOnDeath;
    public GameObject projectile;
    public int coinsReward;
    public int expReward;
}
