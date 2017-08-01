using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Parameter;

namespace CharaMake{
    public class CharaMakeParameterView : MonoBehaviour {
        /// <summary> を表示させるテキスト </summary>
        public Text nameText;
        /// <summary> 説明文を表示させるテキスト </summary>
        public Text descriptionText;
        /// <summary> フレーバーテキストを表示させるテキスト </summary>
        public Text flavorText;

        /// <summary>
        /// データを表示させます
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="description">説明文</param>
        /// <param name="flavorText">フレーバーテキスト</param>
        public void printText(string name, string description, string flavorText) {
            nameText.text = name;
            descriptionText.text = description;
            this.flavorText.text = flavorText;
        }
    }
}
