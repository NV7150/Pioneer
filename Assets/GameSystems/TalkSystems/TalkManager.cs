﻿using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.SceneManagement;using Item;public class TalkManager{    /// <summary> 唯一のインスタンス </summary>    private static readonly TalkManager INSTANCE = new TalkManager();    /// <summary> メッセージウィンドウのプレファブ </summary>    private GameObject massageWindowPrefab;    private bool isTalking = false;    private bool isTrading = false;        /// <summary>    /// 唯一のインスタンスを取得します    /// </summary>    /// <returns>インスタンス</returns>    public static TalkManager getInstance() {        return INSTANCE;    }    /// <summary>    /// シングルトンなのでインスタンス生成不可です    /// </summary>    private TalkManager() {        massageWindowPrefab = (GameObject)Resources.Load("Prefabs/MassageWindow");    }    /// <summary>    /// メッセージを画面に表示します    /// </summary>    /// <param name="massages">表示したいメッセージ</param>    public void talk(List<string> massages){        if (!isTalking) {            GameObject massageWindow = MonoBehaviour.Instantiate(massageWindowPrefab);            massageWindow.transform.SetParent(CanvasGetter.getCanvas().transform);            massageWindow.GetComponent<MassageWindow>().setMassageList(massages);            isTalking = true;        }    }    /// <summary>    /// 会話を終了させます    /// </summary>    public void finishTalk() {        isTalking = false;    }    public void trade(List<string> Introduction,List<string> post,List<IItem> products) {        isTrading = true;    }}