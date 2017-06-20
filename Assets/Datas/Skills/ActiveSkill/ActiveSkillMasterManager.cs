using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	[System.SerializableAttribute]
	public class ActiveSkillMasterManager :MasterDataManagerBase{
		[SerializeField]
		private List<ActiveSkill> dataTable = new List<ActiveSkill>();

		private void Awake(){
			var activeSkillCSVText = Resources.Load("MasterDatas/ActiveSkillMasterData") as TextAsset;
			awakeBehaviour (activeSkillCSVText);
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addToDataList (string[,] datas, int index) {
			dataTable.Add (new ActiveSkill(GetRaw(datas,index)));
		}
		#endregion

		public ActiveSkill getActiveSkillFromId(int id){
			foreach(ActiveSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invlit activeSkillId");
		}
	}
}

