using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;

namespace MasterData{
	[System.SerializableAttribute]
	public class ReactionSkillMasterManager : MasterDataManagerBase{
		//登録済みのデータのリストです
		private static List<ReactionSkill> dataTable = new List<ReactionSkill>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/ReactionSkillMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		//idからReactionスキルを取得します
		public static ReactionSkill getReactionSkillFromId(int id){
			foreach(ReactionSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid ReactionSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
			dataTable.Add(new ReactionSkill (datas));
		}

		#endregion
	}
}
