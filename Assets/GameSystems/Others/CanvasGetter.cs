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
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    // Update is called once per frame
    void Update() {
        if (canvas == null) {
            canvas = GameObject.Find("Canvas");
        }
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene) {
        canvas = null;
    }

    public static GameObject getCanvas() {
        return canvas;
    }
}