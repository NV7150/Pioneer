using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultView : MonoBehaviour {
    public void finishPlaying(){
        PioneerManager.getInstance().finished();
        Destroy(gameObject);
    }
}
