using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;
using Skill;

namespace MasterData {
	public class ReactionSkillSetMasterManager: MasterDataManagerBase{
        private static readonly ReactionSkillSetMasterManager INSTANCE = new ReactionSkillSetMasterManager();

		private ReactionSkillSetMasterManager() {
			var csv = Resources.Load("MasterDatas/ReactionSkillSetMasterData") as TextAsset;
			constractedBehaviour(csv);
        }

        public static ReactionSkillSetMasterManager getInstance(){
            return INSTANCE;
        }

		/// <summary> 登録済みのReactionSkillSetBuilderのリストです </summary>
		private List<ReactionSkillSetBuilder> dataTable = new List<ReactionSkillSetBuilder>();

		//IDからPassiveSkillSetを取得します
		public ReactionSkillSet getReactionSkillSetFromId(int id){
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

