using System;
using System.Collections;
using System.Collections.Generic;

using Character;
using BattleSystem;

using HealAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;
using Extent = Skill.ActiveSkillParameters.Extent;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;
using BattleAbility = Parameter.CharacterParameters.BattleAbility;

namespace Skill {
	public class HealSkill : IActiveSkill{
		private readonly int
			/// <summary> スキルのID </summary>
			ID,
			/// <summary> 回復基本量 </summary>
            RAW_HEAL_VALUE,
			/// <summary> 射程 </summary>
			RANGE,
			/// <summary> MPコスト </summary>
            RAW_COST;

        private int
            healValue,
	        cost;

		private readonly string
			/// <summary> スキル名 </summary>
			NAME,
			/// <summary> スキルの説明文 </summary>
			DESCRIPTION,
	        /// <summary> スキルのフレーバーテキスト </summary>
	        FLAVOR_TEXT;

		/// <summary> ディレイ秒数  </summary>
        private readonly float RAW_DELAY;

        private float delay;

        /// <summary> スキルに使用するBattleAbilty </summary>
		private readonly BattleAbility USE_ABILITY;

        /// <summary> スキルの回復属性 </summary>
		private readonly HealAttribute ATTRIBUTE;

        /// <summary> スキルの効果範囲 </summary>
		private readonly Extent EXTENT;

        private HealSkillObserver observer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
		public HealSkill (string[] datas) {
			ID = int.Parse (datas[0]);
			NAME = datas [1];
			RAW_HEAL_VALUE = int.Parse (datas[2]);
            healValue = RAW_HEAL_VALUE;
			RANGE = int.Parse (datas[3]);
			RAW_DELAY = float.Parse (datas[4]);
            delay = RAW_DELAY;
			RAW_COST = int.Parse (datas[5]);
            cost = RAW_COST;
			ATTRIBUTE = (HealAttribute)Enum.Parse(typeof(HealAttribute),datas [6]);
			EXTENT = (Extent)Enum.Parse(typeof(Extent),datas [7]);
			USE_ABILITY = (BattleAbility)Enum.Parse (typeof(BattleAbility),datas[8]);
			DESCRIPTION = datas [9];
            FLAVOR_TEXT = datas[10];

            observer = new HealSkillObserver(ID);
		}

		/// <summary>
		/// 回復処理を行います
		/// </summary>
		/// <param name="actioner"> 回復を行うIBattleableキャラクター </param>
		/// <param name="targets"> 対象のリスト </param>
		private void heal(IBattleable actioner,List<IBattleable> targets){
			foreach(IBattleable target in targets){
				int healVal = actioner.getHeal(USE_ABILITY);
                target.healed (healVal,ATTRIBUTE);
			}
		}

		/// <summary>
		/// 回復量の基礎値を取得します
		/// </summary>
		/// <returns> 回復基礎値 </returns>
		public int getHeal(){
			return RAW_HEAL_VALUE;
		}

		/// <summary>
		/// スキルの効果範囲を取得します
		/// </summary>
		/// <returns> 効果範囲 </returns>
		public Extent getExtent(){
			return EXTENT;
		}

		/// <summary>
		/// スキルの射程を取得します
		/// </summary>
		/// <returns> 射程 </returns>
		public int getRange(){
			return RANGE;
		}

        public int getRawHeal(){
            return RAW_HEAL_VALUE;
        }

		public float getRawDelay() {
			return RAW_DELAY;
		}

		public int getRawCost() {
			return RAW_COST;
		}

		public void addProgress(ActiveSkillProgress progress) {
            healValue = RAW_HEAL_VALUE + progress.Effect;
            cost = RAW_COST - progress.Cost;
            delay = RAW_DELAY - progress.Delay;
		}

		#region IActiveSkill implementation

		public void action (IBattleable actioner, BattleTask task) {
			if (ActiveSkillSupporter.canUseAffectSkill(actioner, task.getTargets(), this))
				return;
            
			heal (actioner,task.getTargets());
            actioner.minusMp(cost);

            observer.used();
		}

		public int getCost () {
            return cost;
		}

		public float getDelay (IBattleable actioner) {
            return cost;
		}

		public ActiveSkillType getActiveSkillType () {
			return ActiveSkillType.HEAL;
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
            return FLAVOR_TEXT;
        }

        #endregion
    }
}

