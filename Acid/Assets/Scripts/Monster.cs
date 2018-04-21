using UnityEngine;

public class Monster : MonoBehaviour {
    public MonsterScriptableObject monster;
    RaycastHit2D hitInfo;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(SeesThePlayer())
        {
            print("Is close enough: " + IsCloseEnough());
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
}
