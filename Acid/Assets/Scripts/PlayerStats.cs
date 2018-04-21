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
    public int health;

    private void Start()
    {
        instance = this;
        health = stamina * level * staminaToHealthMultiplier * (int)(strength * 0.25);
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

    public void getMoney(int plusMoney)
    {
        copperCoins += plusMoney;
    }
}
