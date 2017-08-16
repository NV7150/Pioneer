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
			/// <summary> このスキルの射程 </summary>
            RAW_RANGE,
            RAW_BONUS,
            RAW_COST;

        private int
	        bonus,
	        cost;

        private float delay;

		private readonly string
			/// <summary> スキル名 </summary>
			NAME,
			/// <summary>  スキルの説明 </summary>
			DESCRIPTION,
	        /// <summary> スキルのフレーバーテキスト </summary>
	        FLAVOR_TEXT;

		private readonly float
			/// <summary> このスキルの効果時間 </summary>
			LIMIT,
            RAW_DELAY;

        /// <summary> スキルの効果範囲 </summary>
		private readonly Extent EXTENT;

        private BufSkillObserver observer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
		public BufSkill (string[] datas) {
			this.ID = int.Parse (datas [0]);
			this.NAME = datas [1];
            this.RAW_BONUS = int.Parse(datas [2]);
            this.bonus = RAW_BONUS;
			this.LIMIT = int.Parse(datas [3]);
			this.RAW_RANGE = int.Parse (datas[4]);
			this.RAW_COST = int.Parse (datas[5]);
            this.cost = RAW_COST;
			this.RAW_DELAY = float.Parse (datas[6]);
            this.delay = RAW_DELAY;
			setBonusParameter (datas[7]);
			this.EXTENT = (Extent)Enum.Parse (typeof(Extent),datas[8]);
			this.DESCRIPTION = datas [9];
            FLAVOR_TEXT = datas[10];

            this.observer = new BufSkillObserver(ID);
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
            this.cost = RAW_COST - progress.Cost;
            this.delay = RAW_DELAY - progress.Delay;
            this.bonus = RAW_BONUS + progress.Effect;
		}

		#region implemented abstract members of SupportSkillBase

		protected override BattleAbilityBonus getAbilityBonus(){
			return new BattleAbilityBonus(NAME,bonusAbility,LIMIT,bonus);
		}

		protected override SubBattleAbilityBonus getSubAbilityBonus(){
			return new SubBattleAbilityBonus (NAME,bonusSubAbility,LIMIT,bonus);
		}

		#endregion

		#region IActiveSkill implementation

		public void action (IBattleable actioner, BattleTask task) {
			if (ActiveSkillSupporter.canUseAffectSkill(actioner, task.getTargets(), this))
				return;

			setBounsToCharacter(task.getTargets());
			actioner.minusMp (this.cost);

            observer.used();
		}

		public int getCost () {
			return cost;
		}

		public float getDelay (IBattleable user) {
			return delay;
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

