using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float lerpSpeed = .03f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            if (target.position != transform.position)
            {
                Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
                newPos = Vector3.Lerp(transform.position, newPos, lerpSpeed);
                newPos.x = Mathf.CeilToInt(newPos.x / 0.1f) * 0.1f;
                newPos.y = Mathf.CeilToInt(newPos.y / 0.1f) * 0.1f;
                newPos.z = Mathf.CeilToInt(newPos.z / 0.1f) * 0.1f;
                transform.position = newPos;
            }
        }
    }
}
