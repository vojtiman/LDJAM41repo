using UnityEngine;

public class Trigger : MonoBehaviour {
    public GameObject enableOnEnter;
    public GameObject enableOnExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            enableOnEnter.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            enableOnExit.SetActive(true);
    }
}
