using UnityEngine;

public class TravelPoint : MonoBehaviour {
    public GameObject whereToGo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = whereToGo.transform.position;
        }
    }
}
