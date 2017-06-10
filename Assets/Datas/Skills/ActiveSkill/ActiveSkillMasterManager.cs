using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using skill;

namespace masterdata {
	public class ActiveSkillMasterManager :MasterDataManagerBase{
		private List<ActiveSkill> dataset = new List<ActiveSkill>();

		private void Awake(){
			var activeSkillCSVText = Resources.Load("Masterdatas/ActiveSkillMasterdata") as TextAsset;
			awakeBehaviour (activeSkillCSVText);
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addToDataList (string[,] datas, int index) {
			dataset.Add (new ActiveSkill(GetRaw(datas,index)));
		}
		#endregion

		public ActiveSkill getActiveSkillFromId(int id){
			foreach(ActiveSkill skill in dataset){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invlit activeSkillId");
		}
	}
}

