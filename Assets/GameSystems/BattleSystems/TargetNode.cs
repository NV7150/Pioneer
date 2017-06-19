using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using character;

namespace battleSystem{
	public class TargetNode : MonoBehaviour {
		public Text textObject;
		List<IBattleable> targets;
		BattleNodeController controller;

		public void setState(IBattleable target,BattleNodeController controller){
			this.targets.Add (target);
			this.controller = controller;
			textObject.text = target.getName ();
		}

		public void setState(FieldPosition pos,BattleNodeController controller){
			this.targets = BattleManager.getInstance().getAreaCharacter(pos);
			this.controller = controller;
			textObject.text = Enum.GetName(typeof(FieldPosition),pos);
		}

		public void chosen(){
			controller.targetChose (targets);
		}
	}
}
