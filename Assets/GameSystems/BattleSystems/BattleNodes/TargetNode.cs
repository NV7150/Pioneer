using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Character;

namespace BattleSystem{
	public class TargetNode : MonoBehaviour {
		//アタッチされているGameObjectの子オブジェクトです
		public Text textObject;
		//担当しているIBattleableキャラクターのリストです
		List<IBattleable> targets = new List<IBattleable> ();
		//もとのPlayerTaskManagerです
		PlayerBattleTaskManager manager;

		//単体を対象としたsetStateです
		public void setState(IBattleable target,PlayerBattleTaskManager controller){
			this.targets.Clear ();
			this.targets.Add (target);
			this.manager = controller;
			textObject.text = target.getName ();
		}

		//エリアを対象としたsetStateです
		public void setState(FieldPosition pos,PlayerBattleTaskManager controller){
			this.targets = BattleManager.getInstance().getAreaCharacter(pos);
			this.manager = controller;
			textObject.text = Enum.GetName(typeof(FieldPosition),pos);
		}

		//選ばれた時の処理です
		public void chosen(){
			manager.targetChose (targets);
		}
	}
}
