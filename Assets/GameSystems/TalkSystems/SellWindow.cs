using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;
using Item;

namespace TalkSystem{
    public class SellWindow : MonoBehaviour, ITradeWindow {
        /// <summary> 取引に参加しているPC </summary>
        private Hero player;
        /// <summary> 取引に参加しているIFriendlyキャラクター </summary>
        private IFriendly trader;
        /// <summary> playerのインベントリ </summary>
        private Inventry inventry;

        /// <summary> tradeItemNodeのプレファブ </summary>
        private GameObject tradeItemNodePrefab;
        /// <summary> スクロールビューのコンテント </summary>
        public GameObject content;
        /// <summary> ヘッダに表示しているテキスト </summary>
        public Text headerText;


        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {

        }

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="player">取引に参加するPC</param>
        /// <param name="trader">取引に参加するフレンドリキャラクター</param>
        public void setState(Hero player, IFriendly trader) {
            this.player = player;
            this.trader = trader;
            this.inventry = player.getInventory();

            headerText.text = player.getName();

            tradeItemNodePrefab = (GameObject)Resources.Load("Prefabs/TradeItemNode");

            foreach (IItem item in inventry.getItems()) {
                GameObject nodeObject = Instantiate(tradeItemNodePrefab);
                TradeItemNode node = nodeObject.GetComponent<TradeItemNode>();
                node.setGoods(item,TradeHelper.getSellValue(item,player,(Merchant)trader), this);
                nodeObject.transform.SetParent(content.transform);
            }
        }

        /// <summary>
        /// 表示させるアイテムを更新します
        /// </summary>
        public void updateItem() {
            detachContents();

            foreach (IItem item in inventry.getItems()) {
                GameObject nodeObject = Instantiate(tradeItemNodePrefab);
                TradeItemNode node = nodeObject.GetComponent<TradeItemNode>();
                node.setGoods(item, TradeHelper.getSellValue(item, player, (Merchant)trader), this);
                nodeObject.transform.SetParent(content.transform);
            }
        }

        /// <summary>
        /// contentsオブジェクトの子ノードを削除します
        /// </summary>
        private void detachContents() {
            Transform children = content.GetComponentInChildren<Transform>();
            foreach (Transform child in children) {
                Destroy(child.gameObject);
            }
            content.transform.DetachChildren();
        }

        /// <summary>
        /// アイテムが選択された時の処理
        /// </summary>
        /// <param name="item">選択されたアイテム</param>
        /// <param name="node">選択されたノード</param>
        public void itemChose(IItem item, TradeItemNode node) {
            int itemValue = TradeHelper.getSellValue(item, player, (Merchant)trader);
            inventry.removeItem(item);
            //かり
            player.addMetal(itemValue);
            Destroy(node.gameObject);
        }
    }
}
