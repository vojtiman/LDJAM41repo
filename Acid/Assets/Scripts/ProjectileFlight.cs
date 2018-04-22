using UnityEngine;

public class ProjectileFlight : MonoBehaviour {
    public int damage;
    public float speed = 30;
    public GameObject target;
    public Vector3 dir;
    public float maxDistance;
    public float delay;

    private Vector3 start;

    // Use this for initialization
    void Start() {
        start = transform.position;
        if (target != null)
        {
            RotateToPlayer();
            if(delay <= 0)
            {
                SendProjectile();
            }
        }
        else
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

            transform.eulerAngles = rot;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }

    void RotateToPlayer()
    {
        Vector3 diff = target.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void SendProjectile()
    {
        GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<Monster>() || collision.transform.GetComponent<PlayerStats>())
        {
            if (collision.transform.GetComponent<Monster>())
                collision.transform.SendMessage("TakeDamage", damage);
            if (collision.transform.GetComponent<PlayerStats>())
                PlayerStats.instance.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
        CheckDistance();
        if (GetComponent<Rigidbody2D>().velocity == Vector2.zero && delay > 0)
            RotateToPlayer();
        if (GetComponent<Rigidbody2D>().velocity == Vector2.zero && delay <= 0)
            SendProjectile();

        delay -= Time.deltaTime;
	}

    void CheckDistance()
    {
        if(Vector3.Distance(start, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
