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
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.LoadScene(sceneName);
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
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        SpawnPlayerIfNeeded();
    }

    
}
