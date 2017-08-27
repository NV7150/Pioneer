using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKeeper : MonoBehaviour {
    private static List<Transform> keepers = new List<Transform>();

    private void Awake(){
        DontDestroyOnLoad(this);
        keepers.Add(transform);
    }

    public static void deleteScene(){
        foreach (var transfrom in keepers){
            foreach(Transform child in transfrom){
                Destroy(child.gameObject);
            }
            transfrom.DetachChildren();
        }
    }
}
