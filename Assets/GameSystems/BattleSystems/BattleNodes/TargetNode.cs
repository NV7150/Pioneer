using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Character;

namespace BattleSystem{
	public class TargetNode : MonoBehaviour {
		/// <summary> アタッチされているオブジェクトのTextオブジェクト </summary>
        public Text targetName;
		/// <summary> 担当しているIBattleableキャラクターのリスト </summary>
		List<IBattleable> targets = new List<IBattleable> ();
		/// <summary> 元のBattleTaskManager </summary>
		PlayerBattleTaskManager manager;

		/// <summary>
        /// 単体を目標とした初期設定
        /// </summary>
        /// <param name="target">担当するキャラクター</param>
        /// <param name="manager">元のBattleTaskManager</param>
        public void setState(IBattleable target,PlayerBattleTaskManager manager){
			this.targets.Clear ();
			this.targets.Add (target);
			this.manager = manager;
			targetName.text = target.getName ();
		}

		/// <summary>
        /// エリアを目標とした初期設定
        /// </summary>
        /// <param name="pos">担当するエリア</param>
        /// <param name="manager">元のBattleTaskManager</param>
        public void setState(FieldPosition pos,PlayerBattleTaskManager manager){
			this.targets = BattleManager.getInstance().getAreaCharacter(pos);
			this.manager = manager;
			targetName.text = Enum.GetName(typeof(FieldPosition),pos);
		}

		/// <summary>
        /// 選ばれた時の処理
        /// </summary>
		public void chosen(){
			manager.targetChose (targets);
		}
	}
}
