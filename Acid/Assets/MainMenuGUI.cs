using UnityEngine.UI;
using UnityEngine;

public class MainMenuGUI : MonoBehaviour {
    public Button startButton;
    public Button loadButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.instance != null)
        {
            startButton.onClick.RemoveAllListeners();
            loadButton.onClick.RemoveAllListeners();

            startButton.onClick.AddListener(StartGame);
            loadButton.onClick.AddListener(LoadGame);
        }
	}

    void StartGame()
    {
        GameManager.instance.ChangeScene("Village");
    }

    void LoadGame()
    {
        GameManager.instance.gameObject.GetComponent<GameSaver>().LoadGame();
    }
}
