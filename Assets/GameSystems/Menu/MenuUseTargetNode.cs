using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;

namespace Menus {
    public class MenuUseTargetNode : MonoBehaviour {
        /// <summary> 担当するキャラクター </summary>
        private IPlayable character;
        /// <summary> 元のウィンドウ </summary>
        private MenuUseWindow window;

        /// <summary> 名前を表示するテキスト </summary>
        public Text nameText;

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="character">表示するキャラクター</param>
        /// <param name="window">元となるウィンドウ</param>
        public void setState(IPlayable character,MenuUseWindow window) {
            this.character = character;
            this.window = window;
        }

        /// <summary>
        /// 選ばれた時の処理
        /// </summary>
        public void chosen() {
            window.targetChosen(character);
        }
    }
}