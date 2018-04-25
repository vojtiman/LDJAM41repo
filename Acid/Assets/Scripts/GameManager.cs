using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject newPlayerPrefab;
    public GameObject gameOverScreen;
    public MakingMoneyController makingMoneyControllerPrefab;
    public GameObject escMenu;

    public bool loaded;

	// Use this for initialization
	void Start () {
        if(GameManager.instance == null)
            instance = this;
        SceneManager.sceneLoaded += OnSceneChanged;
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeScene("Level03");
        }
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
        EscMenu();
    }

    public void ChangeScene(string sceneName)
    {
        if(gameOverScreen != null)
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
                if(loaded)
                {
                    player = Instantiate(playerPrefab, playerSpawner.transform.position, Quaternion.Euler(Vector3.zero));
                }
                else
                {
                    player = Instantiate(newPlayerPrefab, playerSpawner.transform.position, Quaternion.Euler(Vector3.zero));
                }

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
        if(scene.name != "MainMenu")
            SpawnPlayerIfNeeded();
    }

    public void GoToMainMenu()
    {
        SceneManager.sceneLoaded -= OnSceneChanged;
        Destroy(player);
        Destroy(gameObject);
        ChangeScene("MainMenu");
    }

    private void EscMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            escMenu.SetActive(!escMenu.activeSelf);
    }


}
