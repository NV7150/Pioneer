using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace BattleSystem{
	public class MoveAreaNode : MonoBehaviour {
		//アタッチされているGameObjectの子オブジェクトのテキストです
		public Text text;
		//担当するFieldPositionです
		private FieldPosition pos;
		//もとのPlayerBattleTaskManagerです
		private PlayerBattleTaskManager manager;

		//ステートを設定します
		public void setState(FieldPosition pos,PlayerBattleTaskManager manager){
			this.pos = pos;
			this.manager = manager;

			text.text = Enum.GetName(typeof(FieldPosition),pos);
		}

		//選択された時の処理です
		public void chosen(){
			manager.moveAreaChose (pos);
		}
	}
}
