using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;
using Character;
using System;

namespace Menus{
    public class MenuItemNode : MonoBehaviour {
		/// <summary> 担当するアイテム </summary>
		IItem item;
		/// <summary> 担当するアイテムスタック </summary>
		ItemStack stack;
		/// <summary> 元のメニュー </summary>
		Menu menu;

		/// <summary> 担当するアイテムがスタックかのフラグ </summary>
		bool isStack = false;


        /// <summary> 名前を表示させるテキスト </summary>
        public Text nameText;

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="item">担当するアイテム</param>
        /// <param name="menu">元のメニュー</param>
        public void setState(IItem item, Menu menu) {
            this.item = item;
            this.menu = menu;
            nameText.text = item.getName();

            isStack = false;
        }

		/// <summary>
		/// 初期設定を行います
		/// </summary>
		/// <param name="stack">担当するアイテムスタック</param>
		/// <param name="menu">元のメニュー</param>
		public void setState(ItemStack stack ,Menu menu){
            this.stack = stack;
            this.menu = menu;
            nameText.text = stack.getItem().getName();

            isStack = true;
        }

        /// <summary>
        /// 選ばれた時の処理
        /// </summary>
        public void chosen(){
            if (isStack) {
                menu.itemChose(stack);
            } else {
                menu.itemChosen(item);
            }
        }
    }
}
