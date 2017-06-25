using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using AI;

namespace MasterData{
	public class ActiveSkillSetMasterManager : MasterDataManagerBase<ActiveSkillSetBuilder>{

		private void Awake(){
			var activeSkillSetCSV = Resources.Load ("Masterdatas/ActiveSkillSetMasterdata") as TextAsset;
			constractedBehaviour (activeSkillSetCSV);
		}

		public ActiveSkillSet getActiveSkillSetFromId(int id){
			foreach(ActiveSkillSetBuilder builder in dataTable){
				if (builder.getId () == id)
					return builder.build();
			}
			throw new ArgumentException ("invlit id");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override ActiveSkillSetBuilder getInstance (int index, string[,] args) {
			return new ActiveSkillSetBuilder (GetRaw(args,index));
		}

		#endregion
	}
}
