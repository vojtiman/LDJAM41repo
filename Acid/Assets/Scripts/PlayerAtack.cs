using UnityEngine;

public class PlayerAtack : MonoBehaviour {
    public float attackTimer;
    public GameObject projectilePrefab;
    public float maxDistance;
    private float nextAttack;
    private bool ranged = false;

	// Use this for initialization
	void Start () {
		
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
            nextAttack = attackTimer + 2;
        }
    }

    void Attack()
    {
        float dmgMultiplier = 1;
        if (ranged)
            dmgMultiplier = 0.8f;

        Vector2 dir = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.DownArrow))
            dir = new Vector2(0, -1);
        if (Input.GetKey(KeyCode.UpArrow))
            dir = new Vector2(0, 1);
        if (Input.GetKey(KeyCode.LeftArrow))
            dir = new Vector2(-1, 0);
        if (Input.GetKey(KeyCode.RightArrow))
            dir = new Vector2(1, 0);

        if (nextAttack > 0 || dir == new Vector2(0, 0))
            return;

        if(ranged)
        {
            Vector3 pos = transform.position;
            GameObject projectile = Instantiate(projectilePrefab, pos, Quaternion.Euler(Vector3.zero));
            projectile.GetComponent<ProjectileFlight>().damage = PlayerStats.instance.Damage(dmgMultiplier);
            projectile.GetComponent<ProjectileFlight>().dir = dir;
            projectile.layer = 11;
            projectile.GetComponent<ProjectileFlight>().maxDistance = maxDistance;
        }

        if (!ranged)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dir, 32f);
            if (hitInfo.transform == null)
                return;
            if (hitInfo.transform.name == "Teacher")
                hitInfo.transform.GetComponent<Teacher>().OpenTeacherPanel();
            if (hitInfo.transform.GetComponent<Monster>())
                hitInfo.transform.gameObject.SendMessage("TakeDamage", PlayerStats.instance.Damage(dmgMultiplier));
        }

        nextAttack = attackTimer;
    }
}
