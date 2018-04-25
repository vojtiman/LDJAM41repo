using UnityEngine;

public class GameManagerCheck : MonoBehaviour {
    public GameObject gameManager;
    
	// Use this for initialization
	void Start () {
        if (GameManager.instance == null)
            Instantiate(gameManager);
	}
}
