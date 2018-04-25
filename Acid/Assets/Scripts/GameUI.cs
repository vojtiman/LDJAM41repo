using UnityEngine.UI;
using UnityEngine;

public class GameUI : MonoBehaviour {

    [Header("Top right panel")]
    public GameObject topRightPanel;
    public Text copperCoins;
    public Text silverCoins;
    public Text level;

    [Header("Character panel")]
    public GameObject characterPanel;
    public Text Strength;
    public Text Stamina;
    public Text Luck;
    public Text Weapon;
    public Text Armor;
    public Text Damage;
    public Text MaxHealth;
    public Text CritChance;
    public Text DamageReduction;
    public Text ActualHP;

    public Slider expBar;

    [Header("Weapon indicator")]
    public Image weaponIndicator;
    public Sprite[] weapons;

    private bool characterPanelOn = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        UpdateToprightInfo();
        UpdateCharacterPanel();
        UpdateWeaponIndicator();
        if(PlayerStats.instance != null)
        {
            expBar.maxValue = PlayerStats.instance.expNext;
            expBar.value = PlayerStats.instance.experience;
        }
	}

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            characterPanelOn = !characterPanelOn;
        }
    }

    void UpdateToprightInfo()
    {
        if(PlayerStats.instance == null)
        {
            topRightPanel.SetActive(false);
            return;
        }
        else
        {
            topRightPanel.SetActive(true);
            copperCoins.text = PlayerStats.instance.GetCopperCoins().ToString();
            silverCoins.text = PlayerStats.instance.GetSilverCoins().ToString();
            level.text = PlayerStats.instance.level.ToString();
        }
    }

    void UpdateCharacterPanel()
    {
        characterPanel.SetActive(characterPanelOn);
        if (PlayerStats.instance == null)
            return;

        PlayerStats ps = PlayerStats.instance;

        Strength.text = ps.strength.ToString();
        Stamina.text = ps.stamina.ToString();
        Luck.text = ps.luck.ToString();
        Weapon.text = ps.weaponLevel.ToString();
        Armor.text = ps.armorLevel.ToString();
        Damage.text = "(" + ps.MinDamage(1) + " - " + ps.MaxDamage(1) + ")";
        MaxHealth.text = ps.maxHealth.ToString();
        CritChance.text = ps.CritChance() + " %";
        DamageReduction.text = ps.DamageReduction() + "% (Max. 20 %)";
        ActualHP.text = ps.health.ToString();
    }

    void UpdateWeaponIndicator()
    {
        if (PlayerAttack.instance == null)
            return;

        int weapon = 0; //melee
        if (PlayerAttack.instance.ranged)
            weapon = 1;
        weaponIndicator.sprite = weapons[weapon];
    }
}
