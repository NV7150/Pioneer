using System;

using Character;
using BattleSystem;
using Parameter;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using Extent = Skill.ActiveSkillParameters.Extent;

namespace Skill {
	public class DebufSkill : SupportSkillBase,IActiveSkill{
		private readonly int 
			ID,
			BONUS,
			COST,
			RANGE;

		private readonly string
			NAME,
			DESCRIPTION;

		private readonly float 
			DELAY,
			LIMIT;

		private readonly Extent EXTENT;

		public DebufSkill (string[] datas) {
			ID = int.Parse (datas[0]);
			NAME = datas [1];
			BONUS = int.Parse (datas[2]);
			LIMIT = float.Parse (datas[3]);
			COST = int.Parse (datas[4]);
			RANGE = int.Parse (datas[5]);
			DELAY = float.Parse (datas[6]);
			setBonusParameter (datas[7]);
			EXTENT =(Extent) Enum.Parse (typeof(Extent),datas[8]);
			DESCRIPTION = datas [9];
		}

		public Extent getExtent(){
			return EXTENT;
		}

		public int getRange(){
			return RANGE;
		}

		#region implemented abstract members of SupportSkillBase

		protected override BattleAbilityBonus getAbilityBonus () {
			return new BattleAbilityBonus (NAME,bonusAbility,LIMIT,BONUS);
		}

		protected override SubBattleAbilityBonus getSubAbilityBonus () {
			return new SubBattleAbilityBonus (NAME,bonusSubAbility,LIMIT,BONUS);
		}

		#endregion

		#region IActiveSkill implementation

		public void action (IBattleable actioner, BattleTask task) {
			setBounsToCharacter (task.getTargets());
		}

		public int getCost () {
			return COST;
		}

		public float getDelay (IBattleable actioner) {
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

