using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace BattleSystem{
	public class MoveAreaNode : MonoBehaviour {
		/// <summary> アタッチされているオブジェクトのTextオブジェクト </summary>
        public Text areaName;
		/// <summary> 担当するFieldPositon </summary>
        private int pos;
		/// <summary> 元のBattleTaskManger </summary>
		private PlayerBattleTaskManager manager;

		/// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="move">前進は正の数、後退は負の数で移動先を設定します</param>
        /// <param name="manager">元のBattleTaskManager</param>
		public void setState(int move,PlayerBattleTaskManager manager){
            this.pos = move;
			this.manager = manager;

            if (move > 0) {
                areaName.text = move + "つ前進";
            }else if(move < 0){
                areaName.text = -move + "つ後退";
            }else{
                throw new InvalidOperationException("zero moveness");
            }
		}

		/// <summary>
        /// 選択された時の処理
        /// </summary>
		public void chosen(){
			manager.moveAreaChose (pos);
		}
	}
}
