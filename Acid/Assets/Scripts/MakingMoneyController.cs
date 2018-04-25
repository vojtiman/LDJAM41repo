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

    private PlayerStats playerStats;
    public GameObject MakingMoneySpot;
    private CollisionControllerMoneySpot MoneySpotController;

    void Start()
    {
        if(FindObjectOfType<CollisionControllerMoneySpot>())
            MoneySpotController = FindObjectOfType<CollisionControllerMoneySpot>();
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
            if (upgradeLevelWood1 < 5)
            {
                maximumMoney += 1;
            }
            else
                maximumMoney += 2;
            
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
            addingMoney += 4;
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
            addingMoney += 10;
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

    
}
