using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using AI;

using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using ReactionSkillCategory = Skill.ReactionSkillParameters.ReactionSkillCategory;

namespace Skill{
	//受動的に使用するスキルです。
	[System.SerializableAttribute]
	public class ReactionSkill : ISkill{
		[SerializeField]
		private readonly int 
			/// <summary> スキルのID </summary>
			ID,
			/// <summary> スキルの防御修正 </summary>
			DEF,
			/// <summary> スキルの回避修正 </summary>
			DODGE;

        [SerializeField]
        private readonly string
            /// <summary>スキル名</summary>
            NAME,
	        /// <summary> スキルの説明 </summary>
	        DESCRIPTION;

		[SerializeField]
		/// <summary>
        /// カウンターを行うか
        /// 未実装
        /// </summary>
		private readonly bool IS_READY_TO_COUNTER;

		/// <summary> スキルのカテゴリ </summary>
		private ReactionSkillCategory CATEGORY;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="datas">csvによるstring配列</param>
		public ReactionSkill(string[] datas){
			ID = int.Parse(datas [0]);
			NAME = datas [1];
			DEF = int.Parse (datas[2]);
			DODGE = int.Parse (datas[3]);
			IS_READY_TO_COUNTER = (0 == int.Parse (datas [4]));
			DESCRIPTION = datas [5];
			CATEGORY = (ReactionSkillCategory) Enum.Parse (typeof(ReactionSkillCategory), datas [6]);
		}

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

		/// <summary>
		/// userにリアクションを起こさせます
		/// </summary>
		/// <param name="user">リアクションを起こさせるIBattleable</param>
		/// <param name="attack">攻撃を試みるスキルの攻撃値</param>
		/// <param name="hit">攻撃を試みるスキルの命中値</param>
		/// <param name="attribute">攻撃を試みるスキルの属性</param>
		public void reaction (IBattleable user,int attack,int hit,AttackSkillAttribute attribute) {
			if (this.CATEGORY == ReactionSkillCategory.DODGE) {
				//命中判定
				if (hit > user.getDodge () + DODGE)
					//ダメージ処理
					user.dammage (attack, attribute);
			} else if (this.CATEGORY == ReactionSkillCategory.GUARD) {
				int def = user.getDef () + DEF;
				int dammage = attack - def;
				dammage = (dammage >= 0) ? dammage : 0;
				user.dammage (dammage, attribute);
			} else if (this.CATEGORY == ReactionSkillCategory.MISS) {
				user.dammage (attack,attribute);
			}
		}

		/// <summary>
        /// 防御値を取得します
        /// </summary>
        /// <returns>防御値</returns>
		public int getDef(){
			return DEF;
		}

		/// <summary>
        /// 回避値を取得します
        /// </summary>
        /// <returns>回避値</returns>
		public int getDodge(){
			return DODGE;
		}

		/// <summary>
        /// カテゴリを取得します
        /// </summary>
        /// <returns>カテゴリ</returns>
		public ReactionSkillCategory getCategory(){
			return CATEGORY;
		}
	}
}
