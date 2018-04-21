using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float lerpSpeed = .03f;
    // Update is called once per frame
    void LateUpdate()
    {

        if (target.position != transform.position)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed);
        }

    }
}
