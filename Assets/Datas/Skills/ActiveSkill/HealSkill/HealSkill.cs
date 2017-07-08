using System;
using System.Collections;
using System.Collections.Generic;

using Character;
using BattleSystem;

using HealAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;
using Extent = Skill.ActiveSkillParameters.Extent;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Skill {
	public class HealSkill : IActiveSkill{
		private readonly int
			/// <summary> スキルのID </summary>
			ID,
			/// <summary> 回復基本量 </summary>
			HEAL,
			/// <summary> 射程 </summary>
			RANGE,
			/// <summary> ディレイフレーム数  </summary>
			DELAY,
			/// <summary> MPコスト </summary>
			COST;

		private readonly string
			/// <summary> スキル名 </summary>
			NAME,
			/// <summary> スキルの説明文 </summary>
			DESCRIPTION;

		private readonly HealAttribute ATTRIBUTE;

		private readonly Extent EXTENT;

		public HealSkill (string[] datas) {
			ID = int.Parse (datas[0]);
			NAME = datas [1];
			HEAL = int.Parse (datas[2]);
			RANGE = int.Parse (datas[3]);
			DELAY = int.Parse (datas[4]);
			COST = int.Parse (datas[5]);
			DESCRIPTION = datas [6];
			ATTRIBUTE = (HealAttribute)Enum.Parse(typeof(HealAttribute),datas [7]);
			EXTENT = (Extent)Enum.Parse(typeof(Extent),datas [8]);
		}

		/// <summary>
		/// 回復処理を行います
		/// </summary>
		/// <param name="actioner"> 回復を行うIBattleableキャラクター </param>
		/// <param name="targets"> 対象のリスト </param>
		private void heal(IBattleable actioner,List<IBattleable> targets){
			throw new NotSupportedException ();
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
			heal (actioner,task.getTargets());
		}

		public int getCost () {
			return COST;
		}

		public int getDelay (IBattleable actioner) {
			throw new NotImplementedException ();
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

		#endregion
	}
}

