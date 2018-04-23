using UnityEngine.UI;
using UnityEngine;

public class ShowPrice : MonoBehaviour {
    public Text copperCoinsPrice;
    public Text silverCoinsPrice;

    private void Awake()
    {
        copperCoinsPrice = transform.Find("CopperCoinsAmount").GetComponent<Text>();
        silverCoinsPrice = transform.Find("SilverCoinsAmount").GetComponent<Text>();
    }

    // Use this for initialization
    public void UpdatePrices()
    {
        int priceInCopper = 0;
        switch (gameObject.name)
        {
            case "StrengthPanel":
                priceInCopper = Teacher.instance.actualPriceOfStrength;
                break;
            case "StaminaPanel":
                priceInCopper = Teacher.instance.actualPriceOfStamina;
                break;
            case "LuckPanel":
                priceInCopper = Teacher.instance.actualPriceOfLuck;
                break;
            case "ArmorPanel":
                priceInCopper = Teacher.instance.actualPriceOfArmor;
                break;
            case "WeaponPanel":
                priceInCopper = Teacher.instance.actualPriceOfWeapon;
                break;
        }
        copperCoinsPrice.text = (priceInCopper % 100).ToString();
        silverCoinsPrice.text = ((int)(priceInCopper / 100)).ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
