using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;

namespace BattleSystem {
    public class EscapeNode : MonoBehaviour {
        /// <summary> 担当するキャラクター </summary>
        private IBattleable bal;

        /// <summary>
        /// キャラクターを設定します
        /// </summary>
        /// <param name="bal"> 設定するキャラクター </param>
        public void setCharacter(IBattleable bal){
            this.bal = bal;
        }

        /// <summary>
        /// 選ばれた時の処理
        /// </summary>
        public void chosen() {
            if (bal == null)
                throw new InvalidOperationException("character hasn't seted yet");
            BattleManager.getInstance().escapeCommand(bal); 
        }
    }
}
