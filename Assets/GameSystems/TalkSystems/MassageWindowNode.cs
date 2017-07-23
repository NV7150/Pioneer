using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;public class MassageWindowNode : MonoBehaviour {    public Text massageTextObject;    private List<string> massageList = new List<string>();    private Coroutine printCoroutine;    private bool massagePrinting = false;    private int massageIndex = 0;    public GameObject view;    private void Start() {        printCoroutine = StartCoroutine("showText");    }    // Update is called once per frame    void Update () {        if (massageList.Count > 0 ) {            if (Input.GetKeyDown(KeyCode.Return)) {                if (massageList.Count > massageIndex) {
                    if (massagePrinting) {
                        //メッセージをプリントしている場合は全文字表示してコルーチン停止
                        Debug.Log("into massagePringing true");
                        cancelPrint();
                    } else {
                        //違うなら新しく文字を表示
                        Debug.Log("into massagePringing false");
                        printCoroutine = StartCoroutine("showText");
                    }                } else {
                    //インデックスが最後まで来たら削除
                    TalkManager.getInstance().finishTalk();
                    Destroy(view);
                }            }        }	}    /// <summary>    /// テキストを表示するコルーチン    /// </summary>    /// <returns>コルーチンのIEnumertor</returns>    private IEnumerator showText() {        massagePrinting = true;        string massage = massageList[massageIndex];        for(int i = 0;i <= massage.Length; i++) {            yield return new WaitForSeconds(0.1f);            massageTextObject.text = massage.Substring(0,i);        }        massagePrinting = false;        massageIndex++;    }    /// <summary>    /// コルーチンを停止して全文字表示します    /// </summary>    private void cancelPrint() {        Debug.Log("<color=red>cancel!</color>");        StopCoroutine("showText");        massageTextObject.text = massageList[massageIndex];        massageIndex++;        massagePrinting = false;    }    /// <summary>    /// 表示するメッセージを設定します    /// </summary>    /// <param name="massages">表示するメッセージのリスト</param>    public void setMassageList(List<string> massages) {        massageList = massages;    }}