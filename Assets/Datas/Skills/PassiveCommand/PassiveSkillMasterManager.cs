using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;

namespace MasterData{
	[System.SerializableAttribute]
	public class PassiveSkillMasterManager : MasterDataManagerBase{
		private static List<PassiveSkill> dataTable = new List<PassiveSkill>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/PassiveSkillMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		public static PassiveSkill getPassiveSkillFromId(int id){
			foreach(PassiveSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid passiveSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
			dataTable.Add(new PassiveSkill (datas));
		}

		#endregion
	}
}
