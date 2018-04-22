using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Monster : MonoBehaviour {
    public MonsterScriptableObject monsterPrefab;
    public float attackTimer;
    public float nextAttack;
    public LayerMask ignoreLayer = 8;
    public GameObject portalPrefab;
    public List<GameObject> enemyObjects;
    public GameObject boss;

    private MonsterScriptableObject monster;
    private Slider healthBar;
    RaycastHit2D hitInfo;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        monster = Instantiate(monsterPrefab) as MonsterScriptableObject;
        attackTimer = 10f/monster.attackSpeed;
        nextAttack = attackTimer;

        if (!monster.multiObjectEnemy)
        {
            healthBar = GetComponentInChildren<Slider>();
            healthBar.maxValue = monsterPrefab.health;
            healthBar.value = monster.health;
            healthBar.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        EnemyObjectsHealth();
        nextAttack -= Time.deltaTime;
        if (SeesThePlayer())
        {
            if(monster.ranged && IsCloseEnough()) //EDITED
            {
                Stop();
                ShootAtThePlayer();
            }
            else
            {
                if (!IsCloseEnough())
                {
                    MoveTowardsThePlayer();
                }
                else
                {
                    Stop();
                    AttackThePlayer();
                }
            }
        }
        else Stop();

        ShowHP();
	}

    bool SeesThePlayer()
    {
        if (player == null)
            return false;
        hitInfo = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 500 , ~ignoreLayer);
        if(hitInfo.transform != null)
        {
            return hitInfo.transform.CompareTag("Player");
        }

        return false;
    }

    bool IsCloseEnough()
    {
        return (hitInfo.distance <= monster.rangeOrMaxDistance);
    }

    void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    void MoveTowardsThePlayer()
    {
        Vector3 moveVector = player.transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = moveVector.normalized * monster.speed;
    }

    void AttackThePlayer()
    {
        if(nextAttack <= 0)
        {
            player.GetComponent<PlayerStats>().TakeDamage(Random.Range(monster.minDamage, monster.maxDamage));
            nextAttack = attackTimer;
        }
    }

    void TakeDamage(int amount)
    {
        if (monster.multiObjectEnemy && EnemyObjectsHealth() <= 0)
        {
            MonsterDie();
        }
        else if (monster.multiObjectEnemy && EnemyObjectsHealth() > 0) return;

        monster.health -= amount;
        if (monster.health <= 0)
        {
            MonsterDie();
        }
    }

    void MonsterDie()
    {
        if(monster.bossObject)
        {
            boss.GetComponent<Monster>().enemyObjects.Remove(gameObject);
        }
        SpawnPortalOnDeath();
        FindObjectOfType<AudioManager>().Play("MonsterDeath");
        Destroy(gameObject); 
    }

    int EnemyObjectsHealth()
    {
        int health = 0;
        foreach(GameObject obj in enemyObjects)
        {
            if (obj == null)
                enemyObjects.Remove(obj);
            health += obj.GetComponent<Monster>().monster.health;
        }

        return health;
    }

    void SpawnPortalOnDeath()
    {
        if(monster.spawnPortalOnDeath)
        {
            Instantiate(portalPrefab, transform.position, Quaternion.Euler(Vector3.zero));
        }
    }

    void ShowHP()
    {
        if (!monster.multiObjectEnemy)
        {
            healthBar.value = monster.health;
            if (monster.health != monsterPrefab.health)
            {
                healthBar.gameObject.SetActive(true);
            }
        }
    }

    void ShootAtThePlayer()
    {
        if(nextAttack <= 0)
        {
            Vector3 pos = transform.position;

            GameObject projectile = Instantiate(monster.projectile, pos , Quaternion.Euler(Vector3.zero));

            GameObject[] projectiles = new GameObject[projectile.transform.childCount];
            projectile.layer = 8;
            for (int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i] = projectile.transform.GetChild(i).gameObject;
                projectiles[i].layer = 8;
            }

            ProjectileFlight[] flightSettings = projectile.GetComponentsInChildren<ProjectileFlight>();
            for (int i = 0; i < flightSettings.Length; i++)
            {
                flightSettings[i].damage = Random.Range(monster.minDamage, monster.maxDamage);
                flightSettings[i].target = player;
                flightSettings[i].maxDistance = monster.rangeOrMaxDistance;
            }

            nextAttack = attackTimer;
        }
    }
}
