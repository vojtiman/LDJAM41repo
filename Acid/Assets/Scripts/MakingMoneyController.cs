using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class MakingMoneyController : MonoBehaviour {

    public GameObject moneyMaker;
    public GameObject player;
    public Text textSilverCoins;
    public Text textCopperCoins;
    public Text collectText;
    public int maximumMoney = 20;
    public Button unlockButton;
    public GameObject upgrade1;
    public GameObject upgrade2;
    public Button upgrade1Button;
    public Button upgrade2Button;

    public float timeTillNext = 2;
    public float timer = 2;

    private PlayerStats playerStats;
    private int collectebleMoney;
    private int addingMoney = 1;
    void Start()
    {
        // připojení na hráčův script stats
        playerStats = player.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        //úprava textu
        collectText.text = collectebleMoney + "/" + maximumMoney;
        textSilverCoins.text = playerStats.GetSilverCoins().ToString();
        textCopperCoins.text = playerStats.GetCopperCoins().ToString();
        makingMoneyPerMinute();
    }

    public void makingMoneyPerMinute()
    {
        // přidává
        timeTillNext -= Time.deltaTime;
        Debug.Log(timeTillNext);
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
        if(playerStats.GetSilverCoins()>= 1)
        {
            int random = Random.Range(1, 10);
            if(random == 5)
            {
                addingMoney += 1;
            }
            maximumMoney += 1;
            playerStats.getMoney(-100);
        }

    }
    public void upgradeWood1()
    {
        if(playerStats.GetSilverCoins() >= 10)
        {
            int random = Random.Range(1, 8);
            if (random == 5)
                addingMoney += 4;
            maximumMoney += 10;
            playerStats.getMoney(-1000);
        }

        
    }
    public void UpgradeWood2()
    {
        if(playerStats.GetSilverCoins()>=100)
        {
            int random = Random.Range(1, 3);
            if(random == 2)
                addingMoney += 10;
            maximumMoney += 100;
            playerStats.getMoney(-10000);
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
        playerStats.getMoney(1);
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
