using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public static PlayerAttack instance;

    [Header("Static")]
    public float attackTimer;
    public GameObject projectilePrefab;
    public GameObject meleeAttackAnimation;
    public float maxDistance;

    [Header("Runtime")]
    public bool ranged = false;
    public float nextAttack;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        nextAttack -= Time.deltaTime;
        Attack();
        SwitchRangedAndMelee();
	}

    void SwitchRangedAndMelee()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ranged = !ranged;
            nextAttack = attackTimer + 1.2f;
        }
    }

    void Attack()
    {
        Vector2 dir = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.DownArrow))
            dir = new Vector2(0, -1);
        if (Input.GetKey(KeyCode.UpArrow))
            dir = new Vector2(0, 1);
        if (Input.GetKey(KeyCode.LeftArrow))
            dir = new Vector2(-1, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            dir = new Vector2(1, 0);

        if (nextAttack > 0)
            return;
        if (dir == Vector2.zero)
            return;

        PlayerStats.instance.inCombat = true;
        PlayerStats.instance.combatTime = 3;

        float dmgMultiplier = 1;
        float timerMultiplier = 1;
        if (ranged)
        {
            timerMultiplier = 2;
            dmgMultiplier = 0.8f;
        }

        if(ranged)
        {
            Vector3 pos = transform.position;
            GameObject projectile = Instantiate(projectilePrefab, pos, Quaternion.Euler(Vector3.zero));
            FindObjectOfType<AudioManager>().Play("RangedAttack");
            projectile.GetComponent<ProjectileFlight>().damage = PlayerStats.instance.Damage(dmgMultiplier);
            projectile.GetComponent<ProjectileFlight>().dir = dir;
            projectile.layer = 11;
            projectile.GetComponent<ProjectileFlight>().maxDistance = maxDistance;
        }

        if (!ranged)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dir, 32f);

            Instantiate(meleeAttackAnimation, transform.position + (Vector3)(dir * 16), GetRotation(dir));

            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.name == "Teacher")
                    hitInfo.transform.GetComponent<Teacher>().OpenTeacherPanel();
                if (hitInfo.transform.GetComponent<Monster>())
                {
                    FindObjectOfType<AudioManager>().Play("PunchAttack");
                    hitInfo.transform.gameObject.SendMessage("TakeDamage", PlayerStats.instance.Damage(dmgMultiplier));
                }
            }
            else FindObjectOfType<AudioManager>().Play("PunchAir");
        }

        nextAttack = attackTimer * timerMultiplier;
    }

    Quaternion GetRotation(Vector3 dir)
    {
        Vector3 rot = new Vector3(0, 0, 0);

        if (dir == new Vector3(1, 0, 0))
            rot = new Vector3(0, 0, 270);
        if (dir == new Vector3(0, -1, 0))
            rot = new Vector3(0, 0, 180);
        if (dir == new Vector3(-1, 0, 0))
            rot = new Vector3(0, 0, 90);
        if (dir == new Vector3(0, 1, 0))
            rot = new Vector3(0, 0, 0);

        return Quaternion.Euler(rot);
    }
}
