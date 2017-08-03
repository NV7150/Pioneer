using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;using System;using Character;using Item;namespace TalkSystem{	public class MassageWindow : MonoBehaviour {	    /// <summary> テキストを表示するオブジェクト </summary>	    public Text massageTextObject;	    /// <summary> 表示する予定のメッセージのリスト </summary>	    private List<string> massageList = new List<string>();	    private string failMassage;	    /// <summary> 現在表示中のメッセージ </summary>	    private string printingMassage;	    /// <summary> 表示するコルーチン </summary>	    private Coroutine printCoroutine;	    /// <summary> 現在メッセージを表示しているかを表すフラグ </summary>	    private bool massagePrinting = false;	    /// <summary>	    /// massageListに対応するフラグ	    /// printingMassageの一つ先を示す	    /// </summary>	    private int massageIndex = 0;	    /// <summary> 	    /// 取引をする場合にそれを開始するインデックス	    /// 示されたインデックスのメッセージ終了時に取引を開始する	    /// </summary>	    private int startTradeIndex = -1;	    /// <summary> キー操作なしでメッセージの表示の必要があるかのフラグ </summary>	    private bool needToAutoPrint = false;

	    /// <summary> 購入ウィンドウのプレファブ </summary>
	    private GameObject tradeViewPrefab;	    /// <summary> 購入できるアイテムのリスト </summary>        private List<IItem> tradegoods;	    /// <summary> 取引をするプレイヤー </summary>	    private Hero player;	    /// <summary> playerの取引相手であるIFriendlyキャラクター </summary>	    private IFriendly trader;	    /// <summary> 取引中であるかを表すフラグ </summary>	    private bool isTrading = false;	    private void Start() {	        //Debug.Log(trader.getName() + massageList.Count);	        printCoroutine = StartCoroutine(showText(massageList[massageIndex]));	        massageIndex++;
			tradeViewPrefab = (GameObject)Resources.Load("Prefabs/TradeView");	    }

		// Update is called once per frame
		void Update() {
			bool isHavingMassage = (massageList.Count > 0);
			bool isIndexInCount = (massageList.Count > massageIndex);	        if (Input.GetKeyDown(KeyCode.Return)) {	            if (isHavingMassage && isIndexInCount && !isTrading && !massagePrinting){
					printCoroutine = StartCoroutine(showText(massageList[massageIndex]));
					massageIndex++;	            } else if(massagePrinting){
					//メッセージプリント中ならキャンセル
					cancelPrint();	                //トレードしてない状態ならトレード判定してインデックスを進める	                if(!isTrading || isIndexInCount || isHavingMassage){	                    judgeTrade();	                }	            }else if (!isIndexInCount) {
					TalkManager.getInstance().finishTalk();	                Destroy(this.gameObject);	            }	        }else if(needToAutoPrint && isHavingMassage && isIndexInCount){
				printCoroutine = StartCoroutine(showText(massageList[massageIndex]));
				massageIndex++;	            needToAutoPrint = false;	        }

			if (tradeViewPrefab == null) {
				tradeViewPrefab = (GameObject)Resources.Load("Prefabs/TradeView");	        }		}	    /// <summary>	    /// 取引を開始します	    /// </summary>	    private void startTrade() {	        GameObject tradeViewNode = MonoBehaviour.Instantiate(tradeViewPrefab);	        BuyWindow buyWindow = tradeViewNode.GetComponent<TradeView>().getBuyWindow();
	        buyWindow.setState(tradegoods, player, trader,this);
	        //かり
	        tradeViewNode.transform.SetParent(CanvasGetter.getCanvas().transform);	        float posX = (Screen.width / 2 - tradeViewNode.GetComponent<RectTransform>().sizeDelta.x / 2);
	        tradeViewNode.transform.localPosition = new Vector3(posX, 0, 0);	        Canvas.ForceUpdateCanvases();
			isTrading = true;
	    }	    /// <summary>	    /// 取引が必要かを判断し、必要なら開始します	    /// </summary>	    private void judgeTrade(){	        Debug.Log("mi " + (massageIndex - 1) + "t " + startTradeIndex);
	        if (massageIndex - 1 >= 0 && massageIndex - 1 == startTradeIndex) {
				startTrade();
			}	    }	    /// <summary>	    /// テキストを表示するコルーチン	    /// </summary>	    /// <returns>コルーチンのIEnumertor</returns>	    private IEnumerator showText(string massage) {	        massagePrinting = true;	        printingMassage = massage;	        for(int i = 0;i <= massage.Length; i++) {	            yield return new WaitForSeconds(0.1f);	            massageTextObject.text = massage.Substring(0,i);	        }	        massagePrinting = false;
	        if (!isTrading) {
	            judgeTrade();	        }	    }	    /// <summary>	    /// コルーチンを停止して全文字表示します	    /// </summary>	    private void cancelPrint() {	        Debug.Log("<color=red>cancel!</color>");	        StopCoroutine(printCoroutine);
			massagePrinting = false;
			massageTextObject.text = printingMassage;	    }	    /// <summary>	    /// 表示するメッセージを設定します	    /// </summary>	    /// <param name="massages">表示するメッセージのリスト</param>	    public void setMassageList(List<string> massages) {	        massageList = massages;	    }	    /// <summary>	    /// 取引する場合のメッセージを設定します	    /// </summary>	    /// <param name="massages">表示するメッセージのリスト</param>	    /// <param name="tradeIndex">取引するインデックス</param>	    /// <param name="goods">商品のリスト</param>	    /// <param name="player">取引に参加するプレイヤー</param>	    /// <param name="trader">取引に参加するIFriendlyキャラクター</param>	    public void setMassageList(List<string> massages, string failMassage,int tradeIndex,List<IItem> goods, Hero player, IFriendly trader) {
	        massageList = massages;
	        startTradeIndex = tradeIndex;	        this.tradegoods = goods;	        this.player = player;	        this.trader = trader;	        this.failMassage = failMassage;
	    }	    /// <summary>	    /// 取引を終了します	    /// </summary>	    public void tradeFinished() {
	        isTrading = false;	        needToAutoPrint = true;
	    }	    /// <summary>	    /// 金がたりないなどの理由で取引に失敗した時の処理	    /// </summary>	    public void tradeFailed(){	        if (!isTrading)	            throw new InvalidOperationException("isn't trading");	        printCoroutine = StartCoroutine(showText(failMassage));	    }	}}