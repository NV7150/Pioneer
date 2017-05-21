
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using item;
using skill;
using parameter;
using battleSystem;

using System;

namespace character{
	public class Hero :PlayableBase {
		#region implemented abstract members of PlayableBase
		public override void equipWepon (WeponBase wepon) {
			throw new NotImplementedException ();
		}
		public override void equipArmor (ArmorBase armor) {
			throw new NotImplementedException ();
		}
		public override void levelUp () {
			throw new NotImplementedException ();
		}
		#endregion
		

	}
}