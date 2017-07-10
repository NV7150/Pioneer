using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class BufSkillMasterManager : MasterDataManagerBase{
		private static List<BufSkill> dataTable = new List<BufSkill>();

		void Awake(){
			var csv = (TextAsset)Resources.Load ("MasterDatas/BufSkillMasterData");
			constractedBehaviour (csv);
		}

		public static BufSkill getBufSkillFromId(int id){
			foreach(BufSkill skill in dataTable){
				if(skill.getId() == id){
					return skill;
				}
			}
			throw new ArgumentException ("invalid BufSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
			dataTable.Add (new BufSkill(datas));
		}
		#endregion
	}
}

