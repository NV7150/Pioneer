using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using AI;

namespace MasterData{
	public class ActiveSkillSetMasterManager : MasterDataManagerBase{
		private static List<ActiveSkillSetBuilder> dataTable = new List<ActiveSkillSetBuilder>();

		private void Awake(){
			var activeSkillSetCSV = Resources.Load ("MasterDatas/ActiveSkillSetMasterData") as TextAsset;
			constractedBehaviour (activeSkillSetCSV);
		}

		public static ActiveSkillSet getActiveSkillSetFromId(int id){
			foreach(ActiveSkillSetBuilder builder in dataTable){
				if (builder.getId () == id)
					return builder.build();
			}
			throw new ArgumentException ("invlit id");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
			dataTable.Add (new ActiveSkillSetBuilder(datas));
		}

		#endregion
	}
}
