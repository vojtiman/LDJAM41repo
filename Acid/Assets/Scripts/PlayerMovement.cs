using System.Collections;
using System.Collections.Generic;
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

    void Move()
    {
        Vector2 dir = new Vector2(0, 0);

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

        print(dir);
        if (!hitInfo.transform.CompareTag("Player") && !hitInfo.collider.isTrigger)
            return;
        transform.position += (Vector3)dir;
    }
}
