using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using AI;

namespace Skill{
	//受動的に使用するスキルです。
	[System.SerializableAttribute]
	public class PassiveSkill : ISkill{
		[SerializeField]
		private readonly int 
			ID,
			DEF_BOUNS,
			DODGE_BOUNUS;

		[SerializeField]
		private readonly string NAME, DESCRIPTION;

		[SerializeField]
		private readonly bool IS_READY_TO_COUNTER;

		private PassiveSkillCategory CATEGORY;

		public PassiveSkill(string[] datas){
			ID = int.Parse(datas [0]);
			NAME = datas [1];
			DEF_BOUNS = int.Parse (datas[2]);
			DODGE_BOUNUS = int.Parse (datas[3]);
			IS_READY_TO_COUNTER = (0 == int.Parse (datas [4]));
			DESCRIPTION = datas [5];
			CATEGORY = (PassiveSkillCategory) Enum.Parse (typeof(PassiveSkillCategory), datas [6]);
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

		public void use (IBattleable user,int attack,int hit,SkillAttribute attribute) {

			if (this.CATEGORY == PassiveSkillCategory.DODGE) {
				//命中判定
				if (hit > user.getDodgeness () + DODGE_BOUNUS)
					//ダメージ処理
					user.dammage (attack, attribute);
			} else if (this.CATEGORY == PassiveSkillCategory.GUARD) {
				int def = user.getDef () + DEF_BOUNS;
				user.dammage (attack - def, attribute);
			}
		}

		public int getDefBouns(){
			return DEF_BOUNS;
		}

		public int getDodgeBouns(){
			return DODGE_BOUNUS;
		}

		public PassiveSkillCategory getCategory(){
			return CATEGORY;
		}
	}
}
