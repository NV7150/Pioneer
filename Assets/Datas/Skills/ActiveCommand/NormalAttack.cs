using System;
using character;
using battleSystem;

namespace skill {
	public class NormalAttack: IActiveSkill{
		private readonly SkillType TYPE = SkillType.PHYGICAL;
		private readonly string NAME = "normal_attack";
		private readonly string DESCRIPTION = "攻撃します";
		private readonly bool IS_FRIENDLY = false;

		#region ActiveSkill implementation
		public SkillType getSkillType () {
			return TYPE;
		}

		public int getRange (IBattleable user,int basicRange) {
			return basicRange;
		}

		public int getSuccessRate (IBattleable user) {
			return SkillSuporter.getUseAbility (user);
		}

		float IActiveSkill.getDelay (IBattleable user, float basicDelay) {
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
		public int use (IBattleable user) {
			return SkillSuporter.getUseAbility (user);
		}

		bool IActiveSkill.isFriendly () {
			return IS_FRIENDLY;
		}
		#endregion

		public string ToString(){
			return NAME;
		}
	}
}

