using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

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

		public PassiveSkill(string[] datas){
			ID = int.Parse(datas [0]);
			NAME = datas [1];
			DEF_BOUNS = int.Parse (datas[2]);
			DODGE_BOUNUS = int.Parse (datas[3]);
			IS_READY_TO_COUNTER = (0 == int.Parse (datas [4]));
			DESCRIPTION = datas [5];
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
		public void use (IBattleable user) {
			user.setDefBonus (DEF_BOUNS);
			user.setDodBonus (DODGE_BOUNUS);
			user.setIsReadyToCounter (IS_READY_TO_COUNTER);

		}
		#endregion	
	}
}
