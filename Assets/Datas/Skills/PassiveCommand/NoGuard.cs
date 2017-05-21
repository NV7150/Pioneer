using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace skill{
	public class NoGuard : IPassiveSkill {
		private readonly string NAME = "No_Guard";
		private readonly string DESCRIPTION = "ガードせず、攻撃を受けます";

		#region Skill implementation
		public string getName () {
			return NAME;
		}
		public string getDescription () {
			return DESCRIPTION;
		}
		public int use (character.IBattleable user) {
			return 0;
		}
		#endregion
	}
}
