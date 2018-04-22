using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject gameOverScreen;

	// Use this for initialization
	void Start () {
        instance = this;
        SceneManager.sceneLoaded += OnSceneChanged;
        Object.DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnPlayerIfNeeded();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeScene("Level02");
        }
		if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeScene("Level01");
        }
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            ChangeScene("Village");
        }
	}

    public void ChangeScene(string sceneName)
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }

    public void GoBackToVillage()
    {
        player.SetActive(true);
        ChangeScene("Village");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        gameOverScreen.SetActive(true);
    }

    void SpawnPlayerIfNeeded()
    {
        if (player == null)
        {
            if (GameObject.FindGameObjectWithTag("PlayerSpawn") != null)
            {
                GameObject playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawn");
                player = Instantiate(playerPrefab, playerSpawner.transform.position, Quaternion.Euler(Vector3.zero));
                player.GetComponent<PlayerStats>().SetInstance();
            }
        }
        else
        {
            if (!player.activeSelf)
                player.SetActive(true);
        }
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayerIfNeeded();
    }

    
}
