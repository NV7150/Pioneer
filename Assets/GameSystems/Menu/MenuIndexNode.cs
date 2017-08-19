using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SelectView;

using MenuContent = Menus.MenuParameters.MenuContents;

namespace Menus {
    public class MenuIndexNode : MonoBehaviour , INode<MenuContent>{
        /// <summary> 担当するコンテンツ </summary>
        MenuContent content;

        /// <summary> 名前を表示させるテキスト </summary>
        public Text contentText;

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="content">担当するコンテンツ</param>
        public void setContent(MenuContent content){
            this.content = content;

            //かり
            contentText.text = "" + content;
        }

        public MenuContent getElement(){
            return content;
        }
    }
}
