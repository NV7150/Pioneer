using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;
using SelectView;
using System;

namespace Menus{
    public class MenuCharacterNode : MonoBehaviour ,INode<IPlayable>{
        /// <summary> 担当するキャラクター </summary>
        private IPlayable character;

        /// <summary> 名前を表示するテキスト </summary>
        public Text nameText;

        /// <summary>
        /// キャラクターを設定します
        /// </summary>
        /// <param name="character">キャラクター</param>
        public void setCharacter(IPlayable character) {
            this.character = character;

            nameText.text = character.getName();
        }

        public IPlayable getElement() {
            return character;
        }
    }
}
