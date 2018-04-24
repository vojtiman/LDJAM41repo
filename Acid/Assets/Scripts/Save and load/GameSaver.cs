﻿using UnityEngine;

public class GameSaver : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("Saved") != 1) // 1 = saved, 0 = not saved
            return;
        LoadPlayer();
        print("Loading");
    }

    public void SaveGame()
    {
        SavePlayer();
        PlayerPrefs.SetInt("Saved", 1);
    }

    void LoadPlayer()
    {
        GameObject player = GetComponent<GameManager>().playerPrefab;
        PlayerStats ps = player.GetComponent<PlayerStats>();

        ps.armorLevel = PlayerPrefs.GetInt("armorLevel");
        ps.copperCoins = PlayerPrefs.GetInt("copperCoins");
        ps.experience = PlayerPrefs.GetInt("experience");
        ps.expNext = PlayerPrefs.GetInt("expNext");
        ps.level = PlayerPrefs.GetInt("level");
        ps.luck = PlayerPrefs.GetInt("luck");
        ps.maxHealth = PlayerPrefs.GetInt("maxHealth");
        ps.stamina = PlayerPrefs.GetInt("stamina");
        ps.strength = PlayerPrefs.GetInt("strength");
        ps.weaponLevel = PlayerPrefs.GetInt("weaponLevel");

        GetComponent<GameManager>().playerPrefab = player;
    }

    void SavePlayer()
    {
        PlayerStats ps = PlayerStats.instance;
        if (ps == null)
            return;

        PlayerPrefs.SetInt("armorLevel", ps.armorLevel);
        PlayerPrefs.SetInt("copperCoins", ps.copperCoins);
        PlayerPrefs.SetInt("experience", ps.experience);
        PlayerPrefs.SetInt("expNext", ps.expNext);
        PlayerPrefs.SetInt("level", ps.level);
        PlayerPrefs.SetInt("luck", ps.luck);
        PlayerPrefs.SetInt("maxHealth", ps.maxHealth);
        PlayerPrefs.SetInt("stamina", ps.stamina);
        PlayerPrefs.SetInt("strength", ps.strength);
        PlayerPrefs.SetInt("weaponLevel", ps.weaponLevel);
        PlayerPrefs.Save();

        print("Saving");
    }

    void SaveTheMoneyMaker()
    {

    }

    void LoadTheMoneyMaker()
    {

    }
}
