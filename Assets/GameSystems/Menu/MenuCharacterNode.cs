using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;

namespace Menus{
    public class MenuCharacterNode : MonoBehaviour {
        /// <summary> 担当するキャラクター </summary>
        private IPlayable character;
        /// <summary> 元のメニュー </summary>
        private Menu menu;

        /// <summary> 名前を表示するテキスト </summary>
        public Text nameText;

        /// <summary>
        /// キャラクターを設定します
        /// </summary>
        /// <param name="character">キャラクター</param>
        /// <param name="menu">元となるメニュー</param>
        public void setCharacter(IPlayable character, Menu menu) {
            this.character = character;
            this.menu = menu;

            nameText.text = character.getName();
        }

        /// <summary>
        /// 選ばれた時の処理
        /// </summary>
        public void chosen() {
            menu.characterChose(character);
        }
    }
}
