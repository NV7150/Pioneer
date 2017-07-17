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
		private FieldPosition pos;
		/// <summary> 元のBattleTaskManger </summary>
		private PlayerBattleTaskManager manager;

		/// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="pos">担当するFieldPosition</param>
        /// <param name="manager">元のBattleTaskManager</param>
		public void setState(FieldPosition pos,PlayerBattleTaskManager manager){
			this.pos = pos;
			this.manager = manager;

			areaName.text = Enum.GetName(typeof(FieldPosition),pos);
		}

		/// <summary>
        /// 選択された時の処理
        /// </summary>
		public void chosen(){
			manager.moveAreaChose (pos);
		}
	}
}
