using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using Item;

public class TradeWindow : MonoBehaviour {
    /// <summary> スクロールビューのcontent </summary>
    public GameObject content;

    /// <summary> 商品のリスト </summary>
    Goods goods;
    /// <summary> 商品を表示するnodeのプレファブ </summary>
    GameObject tradeItemNodePrefab;
    /// <summary> 取引に参加するプレイヤー </summary>
    Hero player;
    /// <summary> 取引に参加するIFriendlyキャラクター </summary>
    IFriendly trader;
    /// <summary> 親となるメッセージウィンドウ </summary>
    MassageWindow window;

	// Use this for initialization
    void Start () {
        transform.position = new Vector3(Screen.width ,Screen.height,0);
    }

    /// <summary>
    /// 初期設定を行います
    /// </summary>
    /// <param name="goods">商品のリスト</param>
    /// <param name="player">取引に参加するプレイヤー</param>
    /// <param name="trader">取引に参加するIFriendlyキャラクター</param>
    /// <param name="window">親となるメッセージウィンドウ</param>
    public void setState(Goods goods,Hero player,IFriendly trader,MassageWindow window) {
        tradeItemNodePrefab = (GameObject)Resources.Load("Prefabs/TradeItemNode");
        this.goods = goods;
        Debug.Log("gc " + goods.getGoods().Count);
        foreach (IItem item in goods.getGoods()) {
            TradeItemNode node = Instantiate(tradeItemNodePrefab).GetComponent<TradeItemNode>();
            node.setGoods(item,this);
            node.transform.SetParent(content.transform);
        }
        this.player = player;
        this.trader = trader;
        this.window = window;
    }

    /// <summary>
    /// 購入するアイテムが決定した時の処理
    /// </summary>
    /// <param name="item">購入するアイテム</param>
    public void itemChose(IItem item) {
        if (item.getItemValue() > player.getMetal()) {
            window.tradeFailed();
            return;
        }

        player.addItem(goods.getItemFromId(item.getId()));
        player.minusMetal(item.getItemValue());
    }

    /// <summary> 終了ボタンが選択された時の処理 </summary>
    public void finishChose() {
        Debug.Log("into finishchose");
		window.tradeFinished();
        Destroy(gameObject);
    }
}
