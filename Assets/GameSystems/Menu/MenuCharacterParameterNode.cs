using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Menus{
    public class MenuCharacterParameterNode : MonoBehaviour {
        /// <summary> 数値を表示するテキスト </summary>
        public Text numberText;
        /// <summary> パラメータ数値 </summary>
        private int number;

        /// <summary>
        /// 数値を設定します
        /// </summary>
        /// <param name="number">パラメータ</param>
        public void setNumber(int number) {
            this.number = number;
            this.numberText.text = "" + number;
        }
    }
}
