using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;

namespace MasterData{
	[System.SerializableAttribute]
	public class PassiveSkillMasterManager : MasterDataManagerBase {
		[SerializeField]
		private List<PassiveSkill> dataTable = new List<PassiveSkill> ();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/PassiveSkillMasterData") as TextAsset;
			awakeBehaviour (csv);
		}

		public PassiveSkill getPassiveSkillFromId(int id){
			foreach(PassiveSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid passiveSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addToDataList (string[,] datas, int index) {
			dataTable.Add (new PassiveSkill( GetRaw(datas,index)));
		}

		#endregion
	}
}
