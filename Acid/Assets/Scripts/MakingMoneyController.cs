using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class MakingMoneyController : MonoBehaviour {

    public GameObject moneyMaker;
    public GameObject player;
    public Text textMoney;
    public Text collectText;
    public float maximumMoney;
    public Button unlockButton;
    public GameObject upgrade1;
    public GameObject upgrade2;
    public Button upgrade1Button;
    public Button upgrade2Button;

    private bool unlock;
    public float timeTillNext = 60;
    float timer = 60;

    int upgradeCost = 60;
    int upgradeCount;
    private PlayerStats playerStats;
    private float collectebleMoney;
    private float addingMoney = 10;
    void Start()
    {
        // připojení na hráčův script stats
        playerStats = player.GetComponent<PlayerStats>();
        unlock = false;
        upgradeCount = 1;
    }

    private void Update()
    {
        //úprava textu
        collectText.text = collectebleMoney + "/" + maximumMoney + "$";
        textMoney.text = "MONEY: " + playerStats.money + "$";
        if(unlock == true)
        {
            // jestli je odmčeno těžení dřeva přidávají se peníze
            makingMoneyPerMinute();
        }
    }

    public void makingMoneyPerMinute()
    {
        // přidává peníze po minutě
        timeTillNext -= Time.deltaTime;
        Debug.Log(timeTillNext);
        if (timeTillNext <=0)
        {
            if(collectebleMoney < maximumMoney)
            {
                collectebleMoney += addingMoney;
            }
            timeTillNext = timer;
        }
    }



    public void upgradeWood()
    {
        // upgrade tlačítka
        if(unlock == true && playerStats.money >= upgradeCost && upgradeCount == 1)
        {
            playerStats.getMoney(-upgradeCost);
            upgradeCost += 100;
            upgradeCount = 2;
            maximumMoney = 30;
            addingMoney = 15;
            upgrade1Button.image.color = Color.green;
            upgrade2.SetActive(true);

        }
        if (unlock == true && playerStats.money >= upgradeCost && upgradeCount == 2)
        {
            playerStats.getMoney(-upgradeCost);
            upgrade2Button.image.color = Color.green;
            maximumMoney = 40;
            addingMoney = 20;
        }
    }
    public void UnLock()
    {
        // odemkte těžení dřeva
        if(playerStats.money >= 5)
        {
            
            unlock = true;
            playerStats.getMoney(-5);
            
            unlockButton.image.color = Color.green;
            upgrade1.SetActive(true);
        }
        
    }
    public void moneyCollect()
    {
        // tlačítko COLLECT sebere peníze
        playerStats.getMoney(collectebleMoney);
        collectebleMoney = 0;
    }
    public void moneyForClick()
    {
        // Když hráč klikne na tlačítko přičtou se mu peníze
        playerStats.getMoney(.1f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // když přijde hráč na místo spustí UI
        if (collision.CompareTag("Player"))
        {
            Debug.Log("coliduje");
            moneyMaker.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // odstraní UI když hráč opustí místo
        if (collision.CompareTag("Player"))
        {
            moneyMaker.SetActive(false);
        }
    }
}
