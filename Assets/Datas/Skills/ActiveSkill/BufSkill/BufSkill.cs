using System;
using System.Collections;
using System.Collections.Generic;

using Character;
using BattleSystem;
using Parameter;

using Ability = Parameter.CharacterParameters.Ability;
using SubAbility = Parameter.CharacterParameters.SubAbility;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Skill {
	public class BufSkill : SupportSkillBase,IActiveSkill{
		private readonly int
			/// <summary> このスキルのID </summary>
			ID,
			/// <summary> このスキルによる補正値 </summary>
			BONUS,
			/// <summary> このスキルの効果時間 </summary>
			LIMIT,
			/// <summary> このスキルの射程 </summary>
			RANGE,
			/// <summary> このスキルのMPコスト </summary>
			COST,
			DELAY;

		private readonly string
			/// <summary> スキル名 </summary>
			NAME,
			/// <summary>  スキルの説明 </summary>
			DESCRIPTION;

		public BufSkill (string[] datas) {
			this.ID = int.Parse (datas [0]);
			this.NAME = datas [1];
			this.BONUS = int.Parse(datas [2]);
			this.LIMIT = int.Parse(datas [3]);
			this.RANGE = int.Parse (datas[4]);
			this.COST = int.Parse (datas[5]);
			this.DELAY = int.Parse (datas[6]);
			setBonusParameter (datas[7]);
			this.DESCRIPTION = datas [8];
		}

		#region implemented abstract members of SupportSkillBase

		protected override AbilityBonus getAbilityBonus(){
			return new AbilityBonus(NAME,bonusAbility,LIMIT,BONUS);
		}

		protected override SubAbilityBonus getSubAbilityBonus(){
			return new SubAbilityBonus (NAME,bonusSubAbility,LIMIT,BONUS);
		}

		#endregion

		#region IActiveSkill implementation

		public void action (IBattleable actonor, BattleTask task) {
			setBounsToCharacter(task.getTargets());
		}

		public int getCost () {
			return COST;
		}

		public int getDelay (IBattleable user) {
			return DELAY;
		}

		public ActiveSkillType getActiveSkillType () {
			return ActiveSkillType.BUF;
		}

		public bool isFriendly () {
			return true;
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

