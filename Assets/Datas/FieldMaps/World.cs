using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    public List<GameObject> townPositions;
    private GameObject townPrefab;

    private void Awake() {
        townPrefab = (GameObject)Resources.Load("Prefabs/Town");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void creatTown(){
        
    }
}
