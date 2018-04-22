using UnityEngine;

public class Portal : MonoBehaviour {
    public string targetScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Teleport(targetScene);
        }
    }

    private void Teleport(string scene)
    {
        GameManager.instance.ChangeScene(scene);
    }
}
