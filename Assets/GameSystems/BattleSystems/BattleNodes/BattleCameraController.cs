 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour {
    public Camera controllCamera;
    private Camera mainCamera;
	// Use this for initialization
	void Start () {
        
	}

    public void setTransform(Vector3 position){
		GameObject mainCameraObject = GameObject.Find("MainCamera");
        this.mainCamera = mainCameraObject.GetComponent<Camera>();;
        Debug.Log(mainCameraObject);
		if (mainCamera != null) {
			mainCamera.enabled = false;
			controllCamera.enabled = true;
            controllCamera.transform.position = position;
		}
    }

    public void finished(){
        controllCamera.enabled = false;
        mainCamera.enabled = true;
        Destroy(controllCamera.gameObject);
    }
}
