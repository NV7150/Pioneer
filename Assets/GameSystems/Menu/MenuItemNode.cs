using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;
using Character;
using System;
using SelectView;

namespace Menus{
    public class MenuItemNode : MonoBehaviour,INode<IItem> {
		/// <summary> 担当するアイテム </summary>
		IItem item;

        /// <summary> 名前を表示させるテキスト </summary>
        public Text nameText;

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="item">担当するアイテム</param>
        public void setItem(IItem item) {
            this.item = item;
            nameText.text = item.getName();
        }

        public IItem getElement(){
            return item;
        }
    }
}
