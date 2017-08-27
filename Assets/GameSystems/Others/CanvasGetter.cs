using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGetter : MonoBehaviour {
    private static GameObject canvas;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    private void Start() {
        if (canvas != null)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (canvas == null) {
            canvas = GameObject.Find("Canvas");
        }
    }


    public static GameObject getCanvas() {
        return canvas;
    }
}