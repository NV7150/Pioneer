using System;
using character;
using battleSystem;

namespace skill {
	public class NormalAttack: ActiveSkill{
		private readonly SkillType TYPE = SkillType.PHYGICAL;
		private readonly string NAME = "normal_attack";
		private readonly string DESCRIPTION = "攻撃します";
		private readonly bool IS_FRIENDLY = false;

		#region ActiveSkill implementation
		public SkillType getSkillType () {
			return TYPE;
		}

		public int getRange (BattleableBase user,int basicRange) {
			return basicRange;
		}

		public int getSuccessRate (BattleableBase user) {
			return SkillSuporter.getUseAbility (user);
		}

		float ActiveSkill.getDelay (BattleableBase user, float basicDelay) {
			return basicDelay;
		}
		#endregion
		#region Skill implementation
		public string getName () {
			return NAME;
		}
		public string getDescription () {
			return DESCRIPTION;
		}
		public int use (BattleableBase user) {
			return SkillSuporter.getUseAbility (user);
		}

		bool ActiveSkill.isFriendly () {
			return IS_FRIENDLY;
		}
		#endregion

		public string ToString(){
			return NAME;
		}
	}
}

