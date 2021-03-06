﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class DebufSkillMasterManager : MasterDataManagerBase{
        private static readonly DebufSkillMasterManager INSTANCE = new DebufSkillMasterManager();

		private DebufSkillMasterManager() {
			var csv = Resources.Load("MasterDatas/DebufSkillMasterData") as TextAsset;
			constractedBehaviour(csv);
        }

        public static DebufSkillMasterManager getInstance(){
            return INSTANCE;
        }

        /// <summary>
        /// 登録済みのDebufSkillのリスト
        /// </summary>
		private List<DebufSkill> dataTable = new List<DebufSkill>();
        private Dictionary<int, ActiveSkillProgress> progressTable = new Dictionary<int, ActiveSkillProgress>();

        /// <summary>
        /// IDからDebufSkillを取得します
        /// </summary>
        /// <returns>指定されたDebufSkill</returns>
        /// <param name="id">取得したいDebufSkillのID</param>
		public DebufSkill getDebufSkillFromId(int id){
			foreach(DebufSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid DebufSkillId");
		}

        public ActiveSkillProgress getProgressFromId(int id){
            return progressTable[id];
        }

        public void addProgress(int worldId){
            foreach (var builder in dataTable) {
                int id = builder.getId();
                if (ES2.Exists(getLoadPass(id,worldId, "DebufSkillProgress.txt"))) {
					var progress = loadSaveData<ActiveSkillProgress>(id, worldId, "DebufProgress.txt");
					progressTable[id] = progress;
                }
            }
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
            var skill = new DebufSkill(datas);
            dataTable.Add (skill);
            int id = int.Parse(datas[0]);

			SkillBookDataManager.getInstance().setData(skill);
			progressTable.Add(int.Parse(datas[0]), new ActiveSkillProgress());
		}
        #endregion
    }
}

