using UnityEngine;

public class ProjectileFlight : MonoBehaviour {
    public int damage;
    public float speed = 30;
    public GameObject target;
    public Vector3 dir;

	// Use this for initialization
	void Start () {
        if (target != null)
        {
            Vector3 diff = target.transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * speed;
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
		
	}
}
