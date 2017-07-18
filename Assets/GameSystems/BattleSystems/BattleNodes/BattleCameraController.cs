 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour {
    public Camera battleCamera;
    private Camera mainCamera;
	// Use this for initialization
	void Start () {
		GameObject mainCameraObject = GameObject.Find("MainCamera");
		this.mainCamera = mainCameraObject.GetComponent<Camera>(); ;
		Debug.Log(mainCameraObject);
        if (mainCamera != null) {
            mainCamera.enabled = false;
            battleCamera.enabled = true;
        }
	}

    public void finished(){
        battleCamera.enabled = false;
        mainCamera.enabled = true;
        Destroy(battleCamera.gameObject);
    }
}
