using System;

using Character;
using BattleSystem;
using Parameter;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using Extent = Skill.ActiveSkillParameters.Extent;

namespace Skill {
	public class DebufSkill : SupportSkillBase,IActiveSkill{
		private readonly int
			/// <summary> このスキルのID </summary>
			ID,
			/// <summary> このスキルによる補正値 </summary>
            RAW_BONUS,
			/// <summary> このスキルの射程 </summary>
            RAW_RANGE,
			/// <summary> このスキルのMPコスト </summary>
            RAW_COST;

        private int
	        bonus,
	        cost;

        private float
            delay;

		private readonly string
			/// <summary> スキル名 </summary>
			NAME,
			/// <summary>  スキルの説明 </summary>
			DESCRIPTION,
	        /// <summary> スキルのフレーバーテキスト </summary>
	        FLAVOR_TEXT;

		private readonly float
			/// <summary> このスキルのディレイ秒数です </summary>
            RAW_DELAY,
			/// <summary> このスキルの効果時間 </summary>
			LIMIT;

		/// <summary> スキルの効果範囲 </summary>
		private readonly Extent EXTENT;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="datas">csvによるstring配列データ</param>
		public DebufSkill (string[] datas) {
			ID = int.Parse (datas[0]);
			NAME = datas [1];
			RAW_BONUS = int.Parse (datas[2]);
            bonus = RAW_BONUS;
			LIMIT = float.Parse (datas[3]);
			RAW_COST = int.Parse (datas[4]);
            cost = RAW_COST;
			RAW_RANGE = int.Parse (datas[5]);
			RAW_DELAY = float.Parse (datas[6]);
            delay = RAW_DELAY;
			setBonusParameter (datas[7]);
			EXTENT =(Extent) Enum.Parse (typeof(Extent),datas[8]);
			DESCRIPTION = datas [9];
            FLAVOR_TEXT = datas [10];
		}

		/// <summary>
		/// 効果範囲を取得します
		/// </summary>
		/// <returns>効果範囲</returns>
		public Extent getExtent() {
			return EXTENT;
		}

		/// <summary>
		/// スキル射程を取得します
		/// </summary>
		/// <returns>射程</returns>
		public int getRange() {
			return RAW_RANGE;
		}

		public float getRawDelay() {
			return RAW_DELAY;
		}

		public int getRawCost() {
			return RAW_COST;
		}

        public int getRawBonus(){
            return RAW_BONUS;
        }

		public void addProgress(ActiveSkillProgress progress) {
            delay = RAW_DELAY - progress.Delay;
            cost = RAW_COST - progress.Cost;
            bonus = RAW_BONUS + progress.Effect;
		}

		#region implemented abstract members of SupportSkillBase

		protected override BattleAbilityBonus getAbilityBonus () {
            return new BattleAbilityBonus (NAME,bonusAbility,LIMIT,bonus);
		}

		protected override SubBattleAbilityBonus getSubAbilityBonus () {
            return new SubBattleAbilityBonus (NAME,bonusSubAbility,LIMIT,bonus);
		}

		#endregion

		#region IActiveSkill implementation

		public void action (IBattleable actioner, BattleTask task) {
			if (ActiveSkillSupporter.canUseAffectSkill(actioner, task.getTargets(), this))
				return;
            
			setBounsToCharacter (task.getTargets());
            actioner.minusMp (cost);
		}

		public int getCost () {
            return cost;
		}

		public float getDelay (IBattleable actioner) {
            return delay;
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

        public string getFlavorText() {
            return FLAVOR_TEXT;
        }

        #endregion
    }
}

