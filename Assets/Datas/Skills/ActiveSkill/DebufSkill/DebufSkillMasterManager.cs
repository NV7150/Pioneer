using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class DebufSkillMasterManager : MasterDataManagerBase{
		private static List<DebufSkill> dataTable = new List<DebufSkill>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/DebufSkillMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		public static DebufSkill getDebufSkillFromId(int id){
			foreach(DebufSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid DebufSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
			dataTable.Add (new DebufSkill(datas));
		}
		#endregion
	}
}

