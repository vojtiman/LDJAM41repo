using UnityEngine;

public class Monster : MonoBehaviour {
    public MonsterScriptableObject monsterPrefab;
    private MonsterScriptableObject monster;
    private float attackTimer;
    private float nextAttack;
    RaycastHit2D hitInfo;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        monster = Object.Instantiate(monsterPrefab)as MonsterScriptableObject;
        attackTimer = 10/monster.attackSpeed;
        nextAttack = attackTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if(SeesThePlayer())
        {
            if(!IsCloseEnough())
            {
                MoveTowardsThePlayer();
            }
            else
            {
                nextAttack -= Time.deltaTime;
                AttackThePlayer();
            }
        }
	}

    bool SeesThePlayer()
    {
        hitInfo = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if(hitInfo.transform != null)
        {
            return hitInfo.transform.CompareTag("Player");
        }

        return false;
    }

    bool IsCloseEnough()
    {
        return (hitInfo.distance <= monster.range);
    }

    void MoveTowardsThePlayer()
    {
        Vector3 moveVector = player.transform.position - transform.position;
        transform.position += moveVector.normalized * monster.speed * Time.deltaTime;
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
        monster.health -= amount;
        if (monster.health <= 0)
            Destroy(gameObject);
    }
}
