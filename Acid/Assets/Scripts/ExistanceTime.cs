using UnityEngine;

public class ExistanceTime : MonoBehaviour {
    public float time;
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time <= 0)
            Destroy(gameObject);
	}
}
