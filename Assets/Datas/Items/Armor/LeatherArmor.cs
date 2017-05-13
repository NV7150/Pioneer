using System;
using character;

namespace item {
	public class LeatherArmor : Armor{
		private readonly int DEF = 1;
		private readonly int DODGE_BONUS = 0;
		private readonly int NEED_PHY = 0;
		private readonly int VALUE = 30;
		private readonly string NAME = "革の鎧";
		private readonly string DESCRIPTION = "丈夫な革製の鎧";
		private readonly int MASS = 2;

		#region implemented abstract members of Armor
		public override int getDef () {
			return DEF;
		}
		public override int getDodgeBonus () {
			return DODGE_BONUS;
		}
		protected override bool canEquip (Playable user) {
			return user.getPhy () >= NEED_PHY;
		}

		public override string getNeedAbility () {
			return "必要:体力" + NEED_PHY;
		}
			

		#endregion
		#region Item implementation

		public override int getItemValue () {
			return VALUE;
		}

		public override string getName () {
			return NAME;
		}

		public override string getDescription () {
			return DESCRIPTION;
		}


		public override int getMass () {
			return MASS;
		}
		#endregion
	}
}

