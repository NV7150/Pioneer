using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWorkTest : MonoBehaviour {
    private readonly float PAN_LATE = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0, PAN_LATE ,0));
	}
}
