using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MakingMoneyController : MonoBehaviour {

    public GameObject moneyMaker;
    public Text textSilverCoins;
    public Text textCopperCoins;
    public Text collectText;
    public int maximumMoney;
    public Button unlockButton;
    public GameObject upgrade1;
    public GameObject upgrade2;
    public Button upgrade1Button;
    public Button upgrade2Button;

    public float timeTillNext = 1;
    public float timer = 1;


    public int collectebleMoney; // maximální počet peněz, které se vejdou do skladu
    public int addingMoney; //Peníze které se přidají za 1s
    public int upgradeLevelWood1; // dosežený level vylepšení
    public int actualUpgradeLevelWood1; // aktuální level - bude ukazovat kolik vylepšení chybí do dalšího levlu
    public Text UpgradeLVTextWood1;

    public int upgradeLevelWood2; // dosežený level vylepšení
    public int actualUpgradeLevelWood2; // aktuální level - bude ukazovat kolik vylepšení chybí do dalšího levlu
    public Text UpgradeLVTextWood2;

    public int upgradeLevelWood3; // dosežený level vylepšení
    public int actualUpgradeLevelWood3; // aktuální level - bude ukazovat kolik vylepšení chybí do dalšího levlu
    public Text UpgradeLVTextWood3;


    private PlayerStats playerStats;
    public GameObject MakingMoneySpot;
    private CollisionControllerMoneySpot MoneySpotController;

    void Start()
    {
        if(FindObjectOfType<CollisionControllerMoneySpot>())
            MoneySpotController = FindObjectOfType<CollisionControllerMoneySpot>();
        upgradeLevelWood1 = 1;
        upgradeLevelWood2 = 1;
        upgradeLevelWood3 = 1;
        DontDestroyOnLoad(gameObject);
        // připojení na hráčův script stats
        if(PlayerStats.instance != null)
            playerStats = PlayerStats.instance;
    }

    private void Update()
    {
        if (playerStats == null && PlayerStats.instance != null)
            playerStats = PlayerStats.instance;
        if (FindObjectOfType<CollisionControllerMoneySpot>() && MoneySpotController == null)
            MoneySpotController = FindObjectOfType<CollisionControllerMoneySpot>();

        if (MoneySpotController != null)
        {
            if (MoneySpotController.StandingOnTrigger() == true)
            {
                moneyMaker.SetActive(true);
            }
            else
            {
                moneyMaker.SetActive(false);
            }
            //úprava textu
            collectText.text = collectebleMoney + "/" + maximumMoney;
            textSilverCoins.text = playerStats.GetSilverCoins().ToString();
            textCopperCoins.text = playerStats.GetCopperCoins().ToString();
            WriteText(UpgradeLVTextWood1, upgradeLevelWood1, actualUpgradeLevelWood1);
            WriteText(UpgradeLVTextWood2, upgradeLevelWood2, actualUpgradeLevelWood2);
            WriteText(UpgradeLVTextWood3, upgradeLevelWood3, actualUpgradeLevelWood3);

        }
        makingMoneyPerMinute();
    }

    public void makingMoneyPerMinute()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            return;
        // přidává
        timeTillNext -= Time.deltaTime;
        if (timeTillNext <=0)
        {
            if(collectebleMoney < maximumMoney)
            {
                collectebleMoney += addingMoney;
                if (collectebleMoney >= maximumMoney)
                    collectebleMoney = maximumMoney;
                timeTillNext = timer;
            }
            
        }
    }


    public void UpgradeWood()
    {
        if (playerStats == null)
            return;
        if (playerStats.GetSilverCoins()>= 1)
        {
            actualUpgradeLevelWood1 += 1;
            if (actualUpgradeLevelWood1 == upgradeLevelWood1)
            {
                upgradeLevelWood1 += 1;
                actualUpgradeLevelWood1 = 0;
                
                addingMoney += 1;
            }
            
            
            maximumMoney += 1;

            FindObjectOfType<AudioManager>().Play("Upgrade");
            playerStats.GetMoney(-100);
        }

    }
    public void upgradeWood1()
    {
        if (playerStats == null)
            return;
        if (playerStats.GetSilverCoins() >= 10)
        {
            actualUpgradeLevelWood2 += 1;
            if (actualUpgradeLevelWood2 == upgradeLevelWood2)
            {
                upgradeLevelWood2 += 1;
                actualUpgradeLevelWood2 = 0;
                addingMoney += 4;
            }
            maximumMoney += 10;

            FindObjectOfType<AudioManager>().Play("Upgrade");
            playerStats.GetMoney(-1000);
        }
    }
    public void UpgradeWood2()
    {
        if (playerStats == null)
            return;
        if (playerStats.GetSilverCoins()>=100)
        {
            actualUpgradeLevelWood3 += 1;
            if (actualUpgradeLevelWood3 == upgradeLevelWood3)
            {
                upgradeLevelWood3 += 1;
                actualUpgradeLevelWood3 = 0;
                addingMoney += 45;
            }
            maximumMoney += 100;
            
            FindObjectOfType<AudioManager>().Play("Upgrade");
            playerStats.GetMoney(-10000);
        }
    }
    public void moneyCollect()
    {
        // tlačítko COLLECT sebere peníze
        if (playerStats == null)
            return;
        if(collectebleMoney >= 1)
            FindObjectOfType<AudioManager>().Play("MoneyCollect");
        playerStats.GetMoney(collectebleMoney);
        collectebleMoney = 0;
    }
    public void moneyForClick()
    {
        // Když hráč klikne na tlačítko přičtou se mu peníze
        if (playerStats == null)
            return;
        FindObjectOfType<AudioManager>().Play("MoneyForClick");
        playerStats.GetMoney(1);
    }

    private void WriteText(Text textik, int upgradeLevelWood, int actualUpgradeLevelWood)
    {
        if (actualUpgradeLevelWood == 0)
        {
            textik.text = upgradeLevelWood.ToString();
        }
        else
            textik.text = actualUpgradeLevelWood.ToString() + "/" + upgradeLevelWood.ToString();
    }
    
}
