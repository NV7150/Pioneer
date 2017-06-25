using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	[System.SerializableAttribute]
	public class ActiveSkillMasterManager :MasterDataManagerBase<ActiveSkill>{

		private void Start(){
			var activeSkillCSVText = Resources.Load("MasterDatas/ActiveSkillMasterData") as TextAsset;
			constractedBehaviour (activeSkillCSVText);
		}

		public ActiveSkill getActiveSkillFromId(int id){
			foreach(ActiveSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invlit activeSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override ActiveSkill getInstance (int index, string[,] args) {
			return new ActiveSkill (GetRaw(args,index));
		}

		#endregion
	}
}

