using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public float requiredTime = 5;
    public GameObject enemyPrefab;
    private float seeingTime = 0;
    public LayerMask ignoreLayer;
    private GameObject player;
    private RaycastHit2D hitInfo;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        CheckTime();
	}

    void CheckTime()
    {
        if (SeesThePlayer())
        {
            seeingTime += Time.deltaTime;
        }
        else seeingTime = 0;

        if (seeingTime >= requiredTime)
            SpawnEnemy();
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }

    bool SeesThePlayer()
    {
        if (player == null)
            return false;
        hitInfo = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 500, ~ignoreLayer);
        if (hitInfo.transform != null)
        {
            return hitInfo.transform.CompareTag("Player");
        }

        return false;
    }
}
