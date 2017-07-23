using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.SceneManagement;public class TalkManager{    /// <summary> 唯一のインスタンス </summary>    private static readonly TalkManager INSTANCE = new TalkManager();    /// <summary> メッセージウィンドウのプレファブ </summary>    private GameObject massageWindowPrefab;    private bool isTalking = false;        /// <summary>    /// 唯一のインスタンスを取得します    /// </summary>    /// <returns>インスタンス</returns>    public static TalkManager getInstance() {        return INSTANCE;    }        /// <summary>    /// シングルトンなのでインスタンス生成不可です    /// </summary>    private TalkManager() {        massageWindowPrefab = (GameObject)Resources.Load("Prefabs/MassageWindow");    }    /// <summary>    /// メッセージを画面に表示します    /// </summary>    /// <param name="massages"></param>    public void talk(List<string> massages){        if (!isTalking) {
            GameObject massageWindow = MonoBehaviour.Instantiate(massageWindowPrefab);
            massageWindow.transform.SetParent(CanvasGetter.getCanvas().transform);
            massageWindow.GetComponent<MassageWindowNode>().setMassageList(massages);
            isTalking = true;
        }    }    public void finishTalk() {
        isTalking = false;
    }}