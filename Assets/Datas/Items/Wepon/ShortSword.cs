using System.Collections;
using System.Collections.Generic;
using character;

namespace item{
	public class ShortSword : Wepon {
		private readonly int ATTACK = 1;
		private readonly int RANGE = 0;
		private readonly WeponType TYPE = WeponType.SWORD;
		private readonly string NAME = "ショートソード";
		private readonly string DESCRIPTION = "小ぶりな初心者用の剣。";
		private readonly int MASS = 2;
		private readonly int VALUE = 50;
		private readonly int NEED_MFT = 0;
		private readonly float DELAY_TIME = 0.5f;

		#region implemented abstract members of Wepon
		public override int getAttack () {
			return ATTACK;
		}
			
		public override int getRange () {
			return RANGE;
		}

		public override WeponType getWeponType () {
			return TYPE;
		}
			
		protected override bool canEquip (Playable user) {
			return user.getFft () >= NEED_MFT;
		}

		public override string getNeedAbility () {
			return "必要:白兵戦闘力" + NEED_MFT;
		}

		public override float getDelay () {
			return DELAY_TIME;
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
