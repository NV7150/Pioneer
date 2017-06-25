using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Character;

namespace BattleSystem{
	public class TargetNode : MonoBehaviour {
		public Text textObject;
		List<IBattleable> targets;
		PlayerBattleTaskManager controller;

		public void setState(IBattleable target,PlayerBattleTaskManager controller){
			this.targets.Clear ();
			this.targets.Add (target);
			this.controller = controller;
			textObject.text = target.getName ();
		}

		public void setState(FieldPosition pos,PlayerBattleTaskManager controller){
			this.targets = BattleManager.getInstance().getAreaCharacter(pos);
			this.controller = controller;
			textObject.text = Enum.GetName(typeof(FieldPosition),pos);
		}

		public void chosen(){
			controller.targetChose (targets);
		}
	}
}
