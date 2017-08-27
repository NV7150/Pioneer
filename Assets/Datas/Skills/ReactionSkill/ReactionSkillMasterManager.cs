using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;

namespace MasterData{
	[System.SerializableAttribute]
	public class ReactionSkillMasterManager : MasterDataManagerBase{
        private static readonly ReactionSkillMasterManager INSTANCE = new ReactionSkillMasterManager();

		private ReactionSkillMasterManager() {
			var csv = Resources.Load("MasterDatas/ReactionSkillMasterData") as TextAsset;
			constractedBehaviour(csv);
        }

        public static ReactionSkillMasterManager getInstance(){
            return INSTANCE;
        }

		/// <summary>
        /// 登録済みのReactionSkillのリスト
        /// </summary>
		private List<ReactionSkill> dataTable = new List<ReactionSkill>();

		/// <summary>
        /// IDからReactionSkillを取得します
        /// </summary>
        /// <returns>指定されたReactionSkill</returns>
        /// <param name="id">取得したいReactionSkillのID</param>
		public ReactionSkill getReactionSkillFromId(int id){
			foreach(ReactionSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid ReactionSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
            var skill = new ReactionSkill(datas);
            dataTable.Add(skill);
            SkillBookDataManager.getInstance().setData(skill);
		}

        #endregion
    }
}
