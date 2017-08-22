﻿using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.SceneManagement;using System;using Character;using Item;namespace TalkSystem{	public class TalkManager{	    /// <summary> 唯一のインスタンス </summary>	    private static readonly TalkManager INSTANCE = new TalkManager();	    /// <summary> メッセージウィンドウのプレファブ </summary>	    private GameObject massageWindowPrefab;	    /// <summary> 会話中かどうかを示すフラグ </summary>	    private bool isTalking = false;	    	    /// <summary>	    /// 唯一のインスタンスを取得します	    /// </summary>	    /// <returns>インスタンス</returns>	    public static TalkManager getInstance() {	        return INSTANCE;	    }	    /// <summary>	    /// シングルトンなのでインスタンス生成不可です	    /// </summary>	    private TalkManager() {	        massageWindowPrefab = (GameObject)Resources.Load("Prefabs/MassageWindow");	    }	    /// <summary>	    /// メッセージを画面に表示します	    /// </summary>	    /// <param name="massages">表示したいメッセージ</param>	    public void talk(List<string> massages){	        if (!isTalking) {	            GameObject massageWindow = MonoBehaviour.Instantiate(massageWindowPrefab);	            massageWindow.transform.SetParent(CanvasGetter.getCanvas().transform);	            massageWindow.GetComponent<MassageWindow>().setMassageList(massages);	            isTalking = true;	        }	    }	    /// <summary>	    /// 会話を終了させます	    /// </summary>	    public void finishTalk() {	        isTalking = false;	    }	    /// <summary>	    /// 取引を開始します	    /// </summary>	    /// <param name="massages">表示するメッセージのリスト</param>	    /// <param name="startTradeIndex">取引開始のインデックス</param>	    /// <param name="goods">商品のリスト</param>	    /// <param name="player">取引に参加するプレイヤー</param>	    /// <param name="trader">取引に参加するIFriendlyキャラクター</param>        public void trade(List<string> massages,string failMassage,int startTradeIndex,List<IItem> goods,Player player,IFriendly trader) {
			if (!isTalking) {
				GameObject massageWindow = MonoBehaviour.Instantiate(massageWindowPrefab);
				massageWindow.transform.SetParent(CanvasGetter.getCanvas().transform);
				massageWindow.GetComponent<MassageWindow>().setMassageList(massages, failMassage,startTradeIndex, goods, player,trader);
				isTalking = true;
			}	        	    }	}}