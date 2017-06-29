using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;

namespace MasterData {
	public class PassiveSkillSetMasterManager: MasterDataManagerBase{
		private static List<PassiveSkillSetBuilder> dataTable = new List<PassiveSkillSetBuilder>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/PassiveSkillSetMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		public static PassiveSkillSet getPassiveSkillSetFromId(int id){
			foreach(PassiveSkillSetBuilder builder in dataTable){
				if (builder.getId () == id)
					return builder.build ();
			}
			throw new ArgumentException ("invlid passiveSkillSetId");
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
			dataTable.Add (new PassiveSkillSetBuilder(datas));
		}
		#endregion
	}
}

