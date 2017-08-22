using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Parameter;
using System;
using SelectView;

namespace CharaMake {
    public class HumanityNode : MonoBehaviour, INode<Humanity> {
        /// <summary> 名前を表示させるテキスト </summary>
        public Text nameText;

        /// <summary> 担当する人間性 </summary>
        private Humanity humanity;

        /// <summary>
        /// 人間性を設定します
        /// </summary>
        /// <param name="humanity">人間性</param>
        public void setHumanity(Humanity humanity) {
            this.humanity = humanity;

            nameText.text = humanity.getName();
        }

        public Humanity getElement() {
            return humanity;
        }
    }
}
