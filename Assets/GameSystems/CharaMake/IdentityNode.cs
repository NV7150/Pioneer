using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Parameter;
using SelectView;

namespace CharaMake{
    public class IdentityNode : MonoBehaviour, INode<Identity> {
        /// <summary> 担当する特徴 </summary>
        private Identity identity;

        /// <summary> 名前を表示するテキスト </summary>
        public Text nameText;

        /// <summary>
        /// 特徴を設定します
        /// </summary>
        /// <param name="identity">特徴</param>
        public void setIdentity(Identity identity) {
            this.identity = identity;

            nameText.text = identity.getName();
        }

        public Identity getElement() {
            return identity;
        }
    }
}
