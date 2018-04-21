using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float direction;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("w"))
        {
            transform.position += new Vector3(0f, direction, 0f);
        }
        if (Input.GetKeyDown("s"))
        {
            transform.position += new Vector3(0f, -direction, 0f);
        }
        if (Input.GetKeyDown("a"))
        {
            transform.position += new Vector3(-direction, 0f, 0f);
        }
        if (Input.GetKeyDown("d"))
        {
            transform.position += new Vector3(direction, 0f, 0f);
        }
    }

    
}
