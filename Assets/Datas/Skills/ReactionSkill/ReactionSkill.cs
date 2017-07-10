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
			//このスキルのIDです
			ID,
			//このスキルの防御値です
			DEF,
			//このスキルの回避値です
			DODGE;

		[SerializeField]
		private readonly string 
			//このスキルの名前です
			NAME, 
			//このスキルの説明です
			DESCRIPTION;

		[SerializeField]
		//カウンターを行うかを表します
		private readonly bool IS_READY_TO_COUNTER;

		//このスキルのカテゴリです
		private ReactionSkillCategory CATEGORY;

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

		//userにreactionを起こさせます
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

		//防御値を返します
		public int getDef(){
			return DEF;
		}

		//回避値を返します
		public int getDodge(){
			return DODGE;
		}

		//カテゴリを返します
		public ReactionSkillCategory getCategory(){
			return CATEGORY;
		}
	}
}
