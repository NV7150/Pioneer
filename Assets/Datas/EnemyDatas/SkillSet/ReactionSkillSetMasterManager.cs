using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;

namespace MasterData {
	public class ReactionSkillSetMasterManager: MasterDataManagerBase{
		/// <summary> 登録済みのReactionSkillSetBuilderのリストです </summary>
		private static List<ReactionSkillSetBuilder> dataTable = new List<ReactionSkillSetBuilder>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/ReactionSkillSetMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		//IDからPassiveSkillSetを取得します
		public static ReactionSkillSet getReactionSkillSetFromId(int id){
			foreach(ReactionSkillSetBuilder builder in dataTable){
				if (builder.getId () == id)
					return builder.build ();
			}
			throw new ArgumentException ("invalid ReactionSkillSetId");
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
			dataTable.Add (new ReactionSkillSetBuilder(datas));
		}
		#endregion
	}
}

