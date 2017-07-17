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
			HEAL,
			/// <summary> 射程 </summary>
			RANGE,
			/// <summary> MPコスト </summary>
			COST;

		private readonly string
			/// <summary> スキル名 </summary>
			NAME,
			/// <summary> スキルの説明文 </summary>
			DESCRIPTION,
	        /// <summary> スキルのフレーバーテキスト </summary>
	        FLAVOR_TEXT;

		/// <summary> ディレイ秒数  </summary>
		private readonly float DELAY;

        /// <summary> スキルに使用するBattleAbilty </summary>
		private readonly BattleAbility USE_ABILITY;

        /// <summary> スキルの回復属性 </summary>
		private readonly HealAttribute ATTRIBUTE;

        /// <summary> スキルの効果範囲 </summary>
		private readonly Extent EXTENT;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
		public HealSkill (string[] datas) {
			ID = int.Parse (datas[0]);
			NAME = datas [1];
			HEAL = int.Parse (datas[2]);
			RANGE = int.Parse (datas[3]);
			DELAY = float.Parse (datas[4]);
			COST = int.Parse (datas[5]);
			ATTRIBUTE = (HealAttribute)Enum.Parse(typeof(HealAttribute),datas [6]);
			EXTENT = (Extent)Enum.Parse(typeof(Extent),datas [7]);
			USE_ABILITY = (BattleAbility)Enum.Parse (typeof(BattleAbility),datas[8]);
			DESCRIPTION = datas [9];
            FLAVOR_TEXT = datas[10];
		}

		/// <summary>
		/// 回復処理を行います
		/// </summary>
		/// <param name="actioner"> 回復を行うIBattleableキャラクター </param>
		/// <param name="targets"> 対象のリスト </param>
		private void heal(IBattleable actioner,List<IBattleable> targets){
			foreach(IBattleable target in targets){
				int heal = actioner.getHeal(this.USE_ABILITY);
				target.healed (heal,this.ATTRIBUTE);
			}
		}

		/// <summary>
		/// 回復量の基礎値を取得します
		/// </summary>
		/// <returns> 回復基礎値 </returns>
		public int getHeal(){
			return HEAL;
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

		#region IActiveSkill implementation

		public void action (IBattleable actioner, BattleTask task) {
			if (actioner.getMp () < this.COST)
				return;
			heal (actioner,task.getTargets());
			actioner.minusMp(this.COST);
		}

		public int getCost () {
			return COST;
		}

		public float getDelay (IBattleable actioner) {
			return DELAY;
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

