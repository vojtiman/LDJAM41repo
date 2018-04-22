using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats instance;
    [Header("Leveling")]
    public int level = 1;
    public int experience = 0;

    [Header("Stats")]
    public int strength = 1;
    public int stamina = 1;
    public int luck = 1;
    public int armorLevel = 1;
    public int weaponLevel = 1;

    [Header("Multipliers")]
    public int staminaToHealthMultiplier;
    public int strengthToDamageMultiplier;
    public int luckToCritMultiplier;

    [Header("E.T.C.")]
    public int copperCoins;
    public int maxHealth = 0;
    public bool inCombat = false;


    private float health = 0;
    private Slider healthBar;
    private float combatTime;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        maxHealth = stamina * level * staminaToHealthMultiplier * Mathf.CeilToInt(strength * 0.25f);
        health = maxHealth;

        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckCombat();
        ShowHP();
        UpdateStats();
        RegenHP();
    }

    void UpdateStats()
    {
        maxHealth = stamina * level * staminaToHealthMultiplier * Mathf.CeilToInt(strength * 0.25f);

    }

    void CheckCombat()
    {
        combatTime -= Time.deltaTime;
        if (combatTime <= 0)
            inCombat = false;
    }

    void RegenHP()
    {
        if (!inCombat)
        {
            health += maxHealth * 0.05f * Time.deltaTime;
            if (health > maxHealth)
                health = maxHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }

        inCombat = true;
        combatTime = 10;
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

    public int Damage(float dmgMultiplier)
    {
        int Damage = strength * level * strengthToDamageMultiplier;
        Damage = Mathf.CeilToInt(Random.Range(0.8f, 1.2f) * Damage * dmgMultiplier);

        int crit = Random.Range(0, 100);
        if (Mathf.Clamp((luck * luckToCritMultiplier), 0, 50) > crit)
        {
            Damage = (int)(Random.Range(1.5f, 2.5f) * Damage);
            print("Critical damage!! " + Damage);
        }
        return Damage;
    }

    void ShowHP()
    {
        if (health != maxHealth)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
        else healthBar.gameObject.SetActive(false);
    }
}
