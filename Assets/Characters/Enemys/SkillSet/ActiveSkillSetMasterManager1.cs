using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using AI;

namespace masterdata{
	public class ActiveSkillSetMasterManager1 : MasterDataManagerBase{
		List<ActiveSkillSetBuilder> dataset = new List<ActiveSkillSetBuilder>();

		private void Awake(){
			var activeSkillSetCSV = Resources.Load ("Masterdatas/ActiveSkillSetMasterdata") as TextAsset;
			awakeBehaviour (activeSkillSetCSV);
		}

		public ActiveSkillSet getActiveSkillSetFromId(int id){
			foreach(ActiveSkillSetBuilder builder in dataset){
				if (builder.getId () == id)
					return builder.build();
			}
			throw new ArgumentException ("invlit id");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addToDataList (string[,] datas, int index) {
			ActiveSkillSetBuilder builder = new ActiveSkillSetBuilder (GetRaw (datas, index));
			dataset.Add (builder);
		}

		#endregion
	}
}
