using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;

namespace MasterData{
	[System.SerializableAttribute]
	public class PassiveSkillMasterManager : MasterDataManagerBase<PassiveSkill> {

		void Start(){
			var csv = Resources.Load ("MasterDatas/PassiveSkillMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		public PassiveSkill getPassiveSkillFromId(int id){
			foreach(PassiveSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid passiveSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override PassiveSkill getInstance (int index, string[,] args) {
			return new PassiveSkill (GetRaw(args,index));
		}

		#endregion
	}
}
