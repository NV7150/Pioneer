using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pi : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Y)){
            saveTest();
        }
	}

    private void saveTest(){
        HealItemObserver observer = new HealItemObserver(0);
        for (int i = 0; i < 100; i++) {
            observer.usedItem();
        }
        observer.report();
        HealItemObserver.saveToFile();
    }
}
