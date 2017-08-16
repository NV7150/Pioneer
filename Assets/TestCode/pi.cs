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
        Debug.Log("loaded");
		HealItemProgress progress = new HealItemProgress();
        progress.Heal = 10;
        progress.Level = 10;
        progress.ItemValue = 1;

		Debug.Log("loaded");
        ES2Writer writer = ES2Writer.Create("Progresses/HealItemProgeress?tag=Test");
        writer.Write(progress,"Test");
        writer.Save();

        Debug.Log("loaded");
        ES2Reader reader = ES2Reader.Create("Progresses/HealItemProgeress?tag=Test");
        HealItemProgress progressTwo = reader.Read<HealItemProgress>("Test");
        Debug.Log(progressTwo.Heal + " " + progressTwo.Level + " " + progressTwo.ItemValue);
    }
}
