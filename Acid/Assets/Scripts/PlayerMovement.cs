using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
    }
    private void FixedUpdate()
    {
        float timer = 0.15f;
        float timeTillNext = 0.15f;
        timeTillNext -= 1 * Time.deltaTime;
        if(timeTillNext == 0)
        {
            timeTillNext = timer;
        }
        if ((GetComponent<Rigidbody2D>().velocity.x != 0 || GetComponent<Rigidbody2D>().velocity.y != 0) && timeTillNext ==0)
        {
            FindObjectOfType<AudioManager>().Play("Walk");
        }
    }

    void Move()
    {
        Vector2 moveVector = new Vector2(0, 0);

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveVector.x = moveX * 150;
        moveVector.y = moveY * 150;

        GetComponent<Rigidbody2D>().velocity = moveVector;

        /*Vector2 dir = new Vector2(0, 0);

        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = new Vector2(0, speed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dir = new Vector2(0, -speed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dir = new Vector2(-speed, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = new Vector2(speed, 0);
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dir, 32f);

        if (hitInfo.transform != null)
        {
            if (!hitInfo.collider.isTrigger)
                return;
        }
        transform.position += (Vector3)dir;*/
    }
}
