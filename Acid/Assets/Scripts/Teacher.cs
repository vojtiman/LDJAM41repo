using UnityEngine;

public class Teacher : MonoBehaviour {
    public GameObject teacherPanel;
    public float maxDistance = 200;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckDistance();
	}

    public void OpenTeacherPanel()
    {
        teacherPanel.SetActive(true);
    }

    void CheckDistance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, player.transform.position - transform.position, maxDistance + 200);
        if (hitInfo.transform == null)
            return;
        if (hitInfo.distance > maxDistance)
            teacherPanel.SetActive(false);
    }
}
