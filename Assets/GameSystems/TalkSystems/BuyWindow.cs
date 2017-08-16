using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;
using Item;

namespace TalkSystem {
    public class BuyWindow : MonoBehaviour, ITradeWindow {
        /// <summary> スクロールビューのcontent </summary>
        public GameObject content;
        public Text headerText;

        /// <summary> 商品のリスト </summary>
        List<IItem> goods;
        /// <summary> 商品を表示するnodeのプレファブ </summary>
        GameObject tradeItemNodePrefab;
        /// <summary> 取引に参加するプレイヤー </summary>
        Hero player;
        /// <summary> 取引に参加するIFriendlyキャラクター </summary>
        Merchant trader;
        /// <summary> 親となるメッセージウィンドウ </summary>
        MassageWindow window;

        /// <summary> 売却ウィンドウのプレファブ </summary>
        private GameObject sellWindowPrefab;
        /// <summary> 売却ウィンドウ </summary>
        SellWindow sellWindow;

        // Use this for initialization
        void Start() {
            transform.position = new Vector3(Screen.width, Screen.height, 0);
        }

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="goods">商品のリスト</param>
        /// <param name="player">取引に参加するプレイヤー</param>
        /// <param name="trader">取引に参加するIFriendlyキャラクター</param>
        /// <param name="window">親となるメッセージウィンドウ</param>
        public void setState(List<IItem> goods, Hero player, IFriendly trader, MassageWindow window) {
            tradeItemNodePrefab = (GameObject)Resources.Load("Prefabs/TradeItemNode");
            this.goods = goods;
            foreach (IItem item in goods) {
                TradeItemNode node = Instantiate(tradeItemNodePrefab).GetComponent<TradeItemNode>();
                node.setGoods(item, TradeHelper.getBuyValue(item, player, (Merchant)trader), this);
                node.transform.SetParent(content.transform);
            }
            this.player = player;
            this.trader = (Merchant)trader;
            this.window = window;

            headerText.text = trader.getName();

            sellWindow = transform.root.GetComponent<TradeView>().getSellWindow();
            sellWindow.setState(player, trader);
            sellWindow.transform.SetParent(transform.root);
        }

        /// <summary>
        /// 購入するアイテムが決定した時の処理
        /// </summary>
        /// <param name="item">購入するアイテム</param>
        public void itemChose(IItem item, TradeItemNode node) {
            int itemValue = TradeHelper.getBuyValue(item, player, (Merchant)trader);

            if (itemValue > player.getMetal()) {
                window.tradeFailed();
                return;
            }
            player.addItem(item);
            player.minusMetal(itemValue);
            trader.traded(itemValue,item);

            sellWindow.updateItem();
        }

        /// <summary> 終了ボタンが選択された時の処理 </summary>
        public void finishChose() {
            window.tradeFinished(transform.parent.gameObject);
            Destroy(sellWindow.gameObject);
            Destroy(gameObject);
        }
    }
}
