using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicInVillage : MonoBehaviour {
	void Update () {
		if(SceneManager.GetActiveScene().name == "Village")
        {
            FindObjectOfType<AudioManager>().Play("Village");
        }
	}
}
