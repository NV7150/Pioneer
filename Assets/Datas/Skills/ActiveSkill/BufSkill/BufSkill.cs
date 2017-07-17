using System;
using System.Collections;
using System.Collections.Generic;

using Character;
using BattleSystem;
using Parameter;

using Ability = Parameter.CharacterParameters.BattleAbility;
using SubAbility = Parameter.CharacterParameters.SubBattleAbility;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using Extent = Skill.ActiveSkillParameters.Extent;

namespace Skill {
	public class BufSkill : SupportSkillBase,IActiveSkill{
		private readonly int
			/// <summary> このスキルのID </summary>
			ID,
			/// <summary> このスキルによる補正値 </summary>
			BONUS,
			/// <summary> このスキルの射程 </summary>
			RANGE,
			/// <summary> このスキルのMPコスト </summary>
			COST;

		private readonly string
			/// <summary> スキル名 </summary>
			NAME,
			/// <summary>  スキルの説明 </summary>
			DESCRIPTION,
	        /// <summary> スキルのフレーバーテキスト </summary>
	        FLAVOR_TEXT;

		private readonly float 
			/// <summary> このスキルのディレイ秒数です </summary>
			DELAY,
			/// <summary> このスキルの効果時間 </summary>
			LIMIT;

        /// <summary> スキルの効果範囲 </summary>
		private readonly Extent EXTENT;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
		public BufSkill (string[] datas) {
			this.ID = int.Parse (datas [0]);
			this.NAME = datas [1];
			this.BONUS = int.Parse(datas [2]);
			this.LIMIT = int.Parse(datas [3]);
			this.RANGE = int.Parse (datas[4]);
			this.COST = int.Parse (datas[5]);
			this.DELAY = float.Parse (datas[6]);
			setBonusParameter (datas[7]);
			this.EXTENT = (Extent)Enum.Parse (typeof(Extent),datas[8]);
			this.DESCRIPTION = datas [9];
            FLAVOR_TEXT = datas[10];
		}

        /// <summary>
        /// 効果範囲を取得します
        /// </summary>
        /// <returns>効果範囲</returns>
		public Extent getExtent(){
			return EXTENT;
		}

        /// <summary>
        /// スキル射程を取得します
        /// </summary>
        /// <returns>射程</returns>
		public int getRange(){
			return RANGE;
		}

		#region implemented abstract members of SupportSkillBase

		protected override BattleAbilityBonus getAbilityBonus(){
			return new BattleAbilityBonus(NAME,bonusAbility,LIMIT,BONUS);
		}

		protected override SubBattleAbilityBonus getSubAbilityBonus(){
			return new SubBattleAbilityBonus (NAME,bonusSubAbility,LIMIT,BONUS);
		}

		#endregion

		#region IActiveSkill implementation

		public void action (IBattleable actioner, BattleTask task) {
			if (actioner.getMp () < this.COST)
				return;
			setBounsToCharacter(task.getTargets());
			actioner.minusMp (this.COST);
		}

		public int getCost () {
			return COST;
		}

		public float getDelay (IBattleable user) {
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

        public string getFlavorText() {
            throw new NotImplementedException();
        }

        #endregion
    }
}

