using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats instance;
    public int level = 1;
    public int strength = 1;
    public int stamina = 1;
    public int luck = 1;
    public int armorLevel = 1;
    public int weaponLevel = 1;
    public int copperCoins;
    public int experience = 0;

    public int staminaToHealthMultiplier;
    public int strengthToDamageMultiplier;
    public int luckToCritMultiplier;
    public int health = 0;

    private void Start()
    {
        instance = this;
        health = stamina * level * staminaToHealthMultiplier * Mathf.CeilToInt(strength * 0.25f);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public int GetSilverCoins()
    {
        int silverCoinsCount = copperCoins / 100;

        return silverCoinsCount;
    }

    public int GetCopperCoins()
    {
        int copperCoinsCount = copperCoins % 100;
        return copperCoinsCount;
    }

    public void GetMoney(int plusMoney)
    {
        copperCoins += plusMoney;
    }

    public int Damage()
    {
        int Damage = strength * level * strengthToDamageMultiplier;
        Damage = Mathf.CeilToInt(Random.Range(0.8f, 1.2f) * Damage);

        int crit = Random.Range(0, 100);
        if (Mathf.Clamp((luck * luckToCritMultiplier), 0, 50) > crit)
        {
            Damage = (int)(Random.Range(1.5f, 2.5f) * Damage);
            print("Critical damage!! " + Damage);
        }


        return Damage;
    }
}
