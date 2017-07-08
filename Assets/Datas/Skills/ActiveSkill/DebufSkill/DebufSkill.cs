using System;

using Character;
using BattleSystem;
using Parameter;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Skill {
	public class DebufSkill : SupportSkillBase,IActiveSkill{
		private readonly int 
			ID,
			BONUS,
			LIMIT,
			COST,
			RANGE,
			DELAY;

		private readonly string
			NAME,
			DESCRIPTION;

		public DebufSkill (string[] datas) {
			ID = int.Parse (datas[0]);
			NAME = datas [1];
			BONUS = int.Parse (datas[2]);
			LIMIT = int.Parse (datas[3]);
			COST = int.Parse (datas[4]);
			RANGE = int.Parse (datas[5]);
			DELAY = int.Parse (datas[6]);
			setBonusParameter (datas[7]);
			DESCRIPTION = datas [8];
		}

		#region implemented abstract members of SupportSkillBase

		protected override AbilityBonus getAbilityBonus () {
			return new AbilityBonus (NAME,bonusAbility,LIMIT,BONUS);
		}

		protected override SubAbilityBonus getSubAbilityBonus () {
			return new SubAbilityBonus (NAME,bonusSubAbility,LIMIT,BONUS);
		}

		#endregion

		#region IActiveSkill implementation

		public void action (IBattleable actioner, BattleTask task) {
			setBounsToCharacter (task.getTargets());
		}

		public int getCost () {
			return COST;
		}

		public int getDelay (IBattleable actioner) {
			return DELAY;
		}

		public ActiveSkillType getActiveSkillType () {
			return ActiveSkillType.DEBUF;
		}

		public bool isFriendly () {
			return false;
		}

		#endregion

		#region ISkill implementation

		public string getName () {
			return NAME;
		}

		public string getDescription () {
			return DESCRIPTION;
		}

		public int getId () {
			return ID;
		}

		#endregion
	}
}

