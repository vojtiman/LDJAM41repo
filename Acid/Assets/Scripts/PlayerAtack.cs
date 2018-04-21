using UnityEngine;

public class PlayerAtack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Attack();
	}

    void Attack()
    {
        Vector2 dir = new Vector2(0,0);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            dir = new Vector2(0, -1);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            dir = new Vector2(0, 1);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            dir = new Vector2(-1, 0);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            dir = new Vector2(1, 0);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, dir, 32f);
        if (hitInfo.transform == null)
            return;

        print(hitInfo.transform.name);
    }
}
