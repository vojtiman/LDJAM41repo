using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float atackPower;
    public float health;
    public int copperCoins;
    

    public void RemoveHealt(float howMuchHealtRemove)
    {
        health -= howMuchHealtRemove;
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
