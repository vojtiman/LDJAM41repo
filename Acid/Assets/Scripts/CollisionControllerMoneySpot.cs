using UnityEngine;

public class CollisionControllerMoneySpot : MonoBehaviour
{
    private bool StandOnTrigger;
    private void Start()
    {
        StandOnTrigger = false;
        GameManager.instance.gameObject.GetComponent<MakingMoneyController>().MakingMoneySpot = gameObject;
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
