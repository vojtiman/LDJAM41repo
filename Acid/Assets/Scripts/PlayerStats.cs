using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float atackPower;
    public float healt;

    public void RemoveHealt(float howMuchHealtRemove)
    {
        healt -= howMuchHealtRemove;
    }
}
