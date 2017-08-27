using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultView : MonoBehaviour {
    public void finishPlaying(){
        PioneerManager.getInstance().finished();
        SceneManager.LoadScene("Title");
        Destroy(gameObject);
    }
}
