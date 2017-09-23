using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultView : MonoBehaviour {
    public Text resultText;

    public void setText(string text){
        resultText.text = text;
    }

    public void finishPlaying(){
		PioneerManager.getInstance().finished();
        Destroy(gameObject);
    }
}
