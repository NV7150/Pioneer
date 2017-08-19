using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineText : MonoBehaviour {
    private GameObject cameraObject;

	// Use this for initialization
	void Start () {
        cameraObject = GameObject.Find("BattleCamera");
	}
	
	// Update is called once per frame
	void Update () {
        if (cameraObject != null) {
            transform.LookAt(cameraObject.transform);
            this.transform.Rotate(new Vector3(0,180,0));
		} else {
			cameraObject = GameObject.Find("BattleCamera");
        }
	}
}
