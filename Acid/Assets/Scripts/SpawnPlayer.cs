using UnityEngine;

public class SpawnPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
