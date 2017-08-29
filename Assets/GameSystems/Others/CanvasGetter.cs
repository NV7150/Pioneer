using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGetter : MonoBehaviour {
    private static GameObject canvas;
    private static List<Transform> elementComponents = new List<Transform>();
    public List<Transform> componentsList;
    public static GameObject canvasComponent;
    public GameObject canvasComponentObject;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    private void Start() {
        if (canvas != null) {
            Destroy(this.gameObject);
        } else {
            elementComponents.AddRange(componentsList);
            canvasComponent = canvasComponentObject;
        }
    }

    // Update is called once per frame
    void Update() {
        if (canvas == null) {
            canvas = GameObject.Find("Canvas");
        }
    }

    public static GameObject getCanvasElement() {
        return canvasComponent;
    }

	public static void detachCanvasElement() {
        foreach(var canvasnode in elementComponents){
            foreach(Transform child in canvasnode){
                Destroy(child.gameObject);
            }
            canvasnode.DetachChildren();
		}
    }
}