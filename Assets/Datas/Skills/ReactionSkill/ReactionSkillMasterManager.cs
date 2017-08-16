using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;

namespace MasterData{
	[System.SerializableAttribute]
	public class ReactionSkillMasterManager : MasterDataManagerBase{
		/// <summary>
        /// 登録済みのReactionSkillのリスト
        /// </summary>
		private static List<ReactionSkill> dataTable = new List<ReactionSkill>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/ReactionSkillMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		/// <summary>
        /// IDからReactionSkillを取得します
        /// </summary>
        /// <returns>指定されたReactionSkill</returns>
        /// <param name="id">取得したいReactionSkillのID</param>
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
