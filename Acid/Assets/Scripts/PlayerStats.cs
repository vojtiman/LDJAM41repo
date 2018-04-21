using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float atackPower;
    public float health;
    public float money;

    public void RemoveHealt(float howMuchHealtRemove)
    {
        health -= howMuchHealtRemove;
    }

    public void getMoney()
    {
        money += 0.5f;
    }
}
