using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GameObject player;
    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        Object.DontDestroyOnLoad(gameObject);
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            GameObject playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawn");
            player = Instantiate(playerPrefab, playerSpawner.transform.position, Quaternion.Euler(Vector3.zero));
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeScene("Level01");
        }
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            ChangeScene("Vojta");
        }
	}

    public void ChangeScene(string sceneName)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.LoadScene(sceneName);
    }
}
