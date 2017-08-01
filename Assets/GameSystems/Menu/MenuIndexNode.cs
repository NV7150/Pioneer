using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MenuContent = Menus.MenuParameters.MenuContents;

namespace Menus {
    public class MenuIndexNode : MonoBehaviour {
        /// <summary> 担当するコンテンツ </summary>
        MenuContent content;
        /// <summary> 元のメニュー </summary>
        Menu menu;

        /// <summary> 名前を表示させるテキスト </summary>
        public Text contentText;

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="content">担当するコンテンツ</param>
        /// <param name="menu">元のメニュー</param>
        public void setState(MenuContent content,Menu menu){
            this.content = content;
            this.menu = menu;

            //かり
            contentText.text = "" + content;
        }

        /// <summary>
        /// 選ばれた時の処理
        /// </summary>
        public void chosen(){
            menu.indexChosen(content);
        }
    }
}
