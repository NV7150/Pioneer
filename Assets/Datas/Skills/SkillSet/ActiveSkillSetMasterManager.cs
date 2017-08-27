using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using AI;
using Character;
using Skill;

namespace MasterData{
	public class ActiveSkillSetMasterManager : MasterDataManagerBase{
        private static readonly ActiveSkillSetMasterManager INSTANCE = new ActiveSkillSetMasterManager();

		private ActiveSkillSetMasterManager() {
			var activeSkillSetCSV = Resources.Load("MasterDatas/ActiveSkillSetMasterData") as TextAsset;
			constractedBehaviour(activeSkillSetCSV);
        }

		public static ActiveSkillSetMasterManager getInstance() {
			return INSTANCE;
            
        }

		/// <summary> 登録済みのActiveSkillSetBuilderのリスト </summary>
		private List<ActiveSkillSetBuilder> dataTable = new List<ActiveSkillSetBuilder>();

		/// <summary>
        /// IDからActiveSkillSetを取得します
        /// </summary>
        /// <returns>指定されたActiveSkillSet</returns>
        /// <param name="id">取得したいActiveSkillSetのリスト</param>
        /// <param name="user">使用者</param>
		public ActiveSkillSet getActiveSkillSetFromId(int id,IBattleable user){
			foreach(ActiveSkillSetBuilder builder in dataTable){
				if (builder.getId () == id)
					return builder.build(user);
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
