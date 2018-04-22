using UnityEngine.UI;
using UnityEngine;

public class Teacher : MonoBehaviour {
    public static Teacher instance;
    public GameObject teacherPanel;
    public float maxDistance = 200;

    public Text strength;
    public Text stamina;
    public Text luck;
    public Text weaponLevel;
    public Text armorLevel;

    [Header("Price Scaling")]
    public float priceScalingStrength;
    public float priceScalingStamina;
    public float priceScalingLuck;
    public float priceScalingWeapon;
    public float priceScalingArmor;

    [Header("Base price of")]
    public int priceOfStrength;
    public int priceOfStamina;
    public int priceOfLuck;
    public int priceOfWeapon;
    public int priceOfArmor;

    [Header("Actual price of")]
    public int actualPriceOfStrength;
    public int actualPriceOfStamina;
    public int actualPriceOfLuck;
    public int actualPriceOfWeapon;
    public int actualPriceOfArmor;

    private GameObject player;

    // Use this for initialization
    void Start () {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");

        UpdatePricesOfStats();
	}
	
	// Update is called once per frame
	void Update () {
        CheckDistance();
	}

    public void OpenTeacherPanel()
    {
        teacherPanel.SetActive(true);
        UpdateTeacherPanel();
    }

    void UpdateTeacherPanel()
    {
        PlayerStats ps = PlayerStats.instance;
        strength.text = ps.strength.ToString();
        stamina.text = ps.stamina.ToString();
        luck.text = ps.luck.ToString();
        armorLevel.text = ps.armorLevel.ToString();
        weaponLevel.text = ps.weaponLevel.ToString();

        ShowPrice[] priceShowers = FindObjectsOfType<ShowPrice>();
        for (int i = 0; i < priceShowers.Length; i++)
        {
            priceShowers[i].UpdatePrices();
            print(priceShowers[i].name);
        }
    }

    void UpdatePricesOfStats()
    {
        actualPriceOfArmor = CalculatePriceArmor();
        actualPriceOfLuck = CalculatePriceLuck();
        actualPriceOfStamina = CalculatePriceStamina();
        actualPriceOfStrength = CalculatePriceStrength();
        actualPriceOfWeapon = CalculatePriceWeapon();
    }

    void CheckDistance()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, player.transform.position - transform.position, maxDistance + 200);
        if (hitInfo.transform == null)
            return;
        if (hitInfo.distance > maxDistance)
            teacherPanel.SetActive(false);
    }

    #region CalculatePrices
    public int CalculatePriceStrength()
    {
        return (int)(priceOfStrength * PlayerStats.instance.strength * priceScalingStrength);
    }

    public int CalculatePriceStamina()
    {
        return (int)(priceOfStamina * PlayerStats.instance.stamina * priceScalingStamina);
    }

    public int CalculatePriceLuck()
    {
        return (int)(priceOfLuck * PlayerStats.instance.luck * priceScalingLuck);
    }

    public int CalculatePriceArmor()
    {
        return (int)(priceOfArmor * PlayerStats.instance.armorLevel * priceScalingArmor);
    }

    public int CalculatePriceWeapon()
    {
        return (int)(priceOfWeapon * PlayerStats.instance.weaponLevel * priceScalingWeapon);
    }
#endregion

    #region UpgradeStatsFunctions
    public void UpgradeStrength()
    {
        if (PlayerStats.instance.copperCoins >= actualPriceOfStrength)
        {
            PlayerStats.instance.GetMoney(-actualPriceOfStrength);
            PlayerStats.instance.strength += 1;
            UpdatePricesOfStats();
            UpdateTeacherPanel();
        }
        else print("Not enough money mah boii");
    }

    public void UpgradeStamina()
    {
        if (PlayerStats.instance.copperCoins >= actualPriceOfStamina)
        {
            PlayerStats.instance.GetMoney(-actualPriceOfStamina);
            PlayerStats.instance.stamina += 1;
            UpdatePricesOfStats();
            UpdateTeacherPanel();
        }
        else print("Not enough money mah boii");
    }

    public void UpgradeLuck()
    {
        if (PlayerStats.instance.copperCoins >= actualPriceOfLuck)
        {
            PlayerStats.instance.GetMoney(-actualPriceOfLuck);
            PlayerStats.instance.luck += 1;
            UpdatePricesOfStats();
            UpdateTeacherPanel();
        }
        else print("Not enough money mah boii");
    }

    public void UpgradeArmor()
    {
        if (PlayerStats.instance.copperCoins >= actualPriceOfArmor)
        {
            PlayerStats.instance.GetMoney(-actualPriceOfArmor);
            PlayerStats.instance.armorLevel += 1;
            UpdatePricesOfStats();
            UpdateTeacherPanel();
        }
        else print("Not enough money mah boii");
    }

    public void UpgradeWeapon()
    {
        if (PlayerStats.instance.copperCoins >= actualPriceOfWeapon)
        {
            PlayerStats.instance.GetMoney(-actualPriceOfWeapon);
            PlayerStats.instance.weaponLevel += 1;
            UpdatePricesOfStats();
            UpdateTeacherPanel();
        }
        else print("Not enough money mah boii");
    }

#endregion
}
