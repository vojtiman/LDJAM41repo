using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MakingMoneyController : MonoBehaviour {

    public GameObject moneyMaker;
    public GameObject player;
    public Text textMoney;
    

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        textMoney.text = "MONEY: " + playerStats.money;
    }






    public void moneyForClick()
    {
        playerStats.getMoney();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("coliduje");
            moneyMaker.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("coliduje");
            moneyMaker.SetActive(false);
        }
    }
}
