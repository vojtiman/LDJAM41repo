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

    private bool characterPanelOn = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        UpdateToprightInfo();
        UpdateCharacterPanel();
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

        Strength.text = PlayerStats.instance.strength.ToString();
        Stamina.text = PlayerStats.instance.stamina.ToString();
        Luck.text = PlayerStats.instance.luck.ToString();
        Weapon.text = PlayerStats.instance.weaponLevel.ToString();
        Armor.text = PlayerStats.instance.armorLevel.ToString();
    }
}
