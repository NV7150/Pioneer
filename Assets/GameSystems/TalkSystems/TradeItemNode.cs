using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;

namespace TalkSystem {
    public class TradeItemNode : MonoBehaviour {
        /// <summary> 担当するアイテム </summary>
        private IItem item;
        /// <summary> アイテム名を表示するテキストオブジェクト </summary>
        public Text name;
        /// <summary> アイテム価格を表示するテキストオブジェクト </summary>
        public Text value;
        /// <summary> 親となるトレードウィンドウ </summary>
        private ITradeWindow window;

        /// <summary>
        /// 商品とウィンドウを設定します
        /// </summary>
        /// <param name="item">商品</param>
        /// <param name="window">親となるウィンドウ</param>
        public void setGoods(IItem item,int itemValue, ITradeWindow window) {
            this.item = item;
            name.text = this.item.getName();
            //かり
            value.text = "" + itemValue;
            this.window = window;
        }

        /// <summary>
        /// 選ばれた時の処理
        /// </summary>
        public void chosen() {
            if (item != null) {
                window.itemChose(item, this);
            }
        }
    }
}