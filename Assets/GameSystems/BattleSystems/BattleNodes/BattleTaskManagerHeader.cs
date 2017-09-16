using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTaskManagerHeader : MonoBehaviour {
    private static readonly float BLINK_INTERBAL = 0.5f;

    public Image header;

    private bool isBlinking = false;
    private bool needToBlink = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (needToBlink) {
            if (!isBlinking) {
                StartCoroutine(blink());
            }
        }else{
            header.color = Color.white;
        }
	}

    private IEnumerator blink(){
        isBlinking = true;
        header.color = Color.red;
        yield return new WaitForSeconds(BLINK_INTERBAL);
        header.color = Color.white;
        yield return new WaitForSeconds(BLINK_INTERBAL);
        isBlinking = false;
    }

    public void changeBlinkState(bool state){
        this.needToBlink = state;
    }
}
