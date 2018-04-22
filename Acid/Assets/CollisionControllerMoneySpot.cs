using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControllerMoneySpot : MonoBehaviour
{
    private bool StandOnTrigger;
    private GameObject player;
    private void Start()
    {
        StandOnTrigger = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StandOnTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StandOnTrigger = false;
        }
    }

    public bool StandingOnTrigger()
    {
        return StandOnTrigger;
    }
}
