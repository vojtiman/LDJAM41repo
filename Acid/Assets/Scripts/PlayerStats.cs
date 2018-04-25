using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats instance;
    [Header("Leveling")]
    public int level = 1;
    public int experience = 0;
    public int expNext = 500;

    [Header("Stats")]
    public int strength = 1;
    public int stamina = 1;
    public int luck = 1;
    public int armorLevel = 1;
    public int weaponLevel = 1;

    [Header("Multipliers")]
    public float staminaToHealthMultiplier;
    public float strengthToDamageMultiplier;
    public float strengthToHealthMultiplier;
    public int luckToCritMultiplier;


    [Header("E.T.C.")]
    public int copperCoins;
    public int maxHealth = 0;
    public int health = 0;
    public bool inCombat = false;
    public float combatTime;
    public bool criticalDamage = false;

    private Slider healthBar;
    private float regenTime = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        maxHealth = Mathf.Clamp(Mathf.RoundToInt(stamina * staminaToHealthMultiplier + level * 0.25f * staminaToHealthMultiplier + strengthToHealthMultiplier * strength * level * 0.25f), 1, 999999999);
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
        maxHealth = Mathf.Clamp(Mathf.RoundToInt(stamina * staminaToHealthMultiplier + level * 0.25f * stamina * staminaToHealthMultiplier + strengthToHealthMultiplier * strength * level * 0.25f), 1, 999999999);
    }

    void CheckCombat()
    {
        combatTime -= Time.deltaTime;
        if (combatTime <= 0)
            inCombat = false;
    }

    void RegenHP()
    {
        regenTime -= Time.deltaTime;

        if (!inCombat && regenTime <= 0)
        {
            health += Mathf.CeilToInt(maxHealth * 0.15f);
            if (health > maxHealth)
                health = maxHealth;
            regenTime = 1;
        }
    }

    public void TakeDamage(int amount)
    {
        float reduction = 1;
        if(!GetComponent<PlayerAttack>().ranged)
            reduction = 1 - (0.02f * armorLevel);
        amount = Mathf.CeilToInt(amount * reduction);

        health -= amount;
        if (health <= 0)
        {
            GameManager.instance.GameOver();
            health = Mathf.FloorToInt(maxHealth * 0.5f);
            gameObject.SetActive(false);
        }

        inCombat = true;
        combatTime = 3;
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
        int Damage = Mathf.Clamp(Mathf.RoundToInt(strength * strengthToDamageMultiplier * (level * 0.15f * strengthToDamageMultiplier)), 1, 999999999);
        Damage = Mathf.CeilToInt(Random.Range(0.8f, 1.2f) * Damage * dmgMultiplier * weaponLevel);

        int crit = Random.Range(0, 100);
        if (Mathf.Clamp((luck * luckToCritMultiplier), 0, 50) > crit)
        {
            Damage = (int)(Random.Range(1.5f, 2.5f) * Damage);
            criticalDamage = true;
        }
        else criticalDamage = false;

        return Damage;
    }

    public int MaxDamage(float dmgMultiplier)
    {
        int Damage = Mathf.Clamp(Mathf.RoundToInt(strength * strengthToDamageMultiplier * (level * 0.15f * strengthToDamageMultiplier)), 1, 999999999);
        Damage = Mathf.CeilToInt(1.2f * Damage * dmgMultiplier * weaponLevel);
        return Damage;
    }

    public int MinDamage(float dmgMultiplier)
    {
        int Damage = Mathf.Clamp(Mathf.RoundToInt(strength * strengthToDamageMultiplier * (level * 0.15f * strengthToDamageMultiplier)), 1, 999999999);
        Damage = Mathf.CeilToInt(0.8f * Damage * dmgMultiplier * weaponLevel);
        return Damage;
    }

    public int CritChance()
    {
        int critChance = luck * luckToCritMultiplier;
        return critChance;
    }

    public int DamageReduction()
    {
        int dmgReduction = armorLevel * 2;
        return dmgReduction;
    }

    void ShowHP()
    {
        if (health != maxHealth)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.maxValue = maxHealth;
            healthBar.value = Mathf.CeilToInt(health);
        }
        else healthBar.gameObject.SetActive(false);
    }

    public void SetInstance()
    {
        instance = this;
    }

    public void AddExp(int amount)
    {
        experience += amount;
        if(experience >= expNext)
        {
            experience -= expNext;
            level++;
            GetComponent<PlayerSpriteChanger>().UpdateSprite(level);
            expNext = Mathf.CeilToInt(expNext * 1.4f);
        }
    }

    public void ResetPlayer()
    {
        health = maxHealth;
    }   
}
