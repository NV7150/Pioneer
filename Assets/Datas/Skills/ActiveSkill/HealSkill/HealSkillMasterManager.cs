using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class HealSkillMasterManager : MasterDataManagerBase{
        private static readonly HealSkillMasterManager INSTANCE = new HealSkillMasterManager();

		private HealSkillMasterManager() {
			var csv = Resources.Load("MasterDatas/HealSkillMasterData") as TextAsset;
			constractedBehaviour(csv);
        }

        public static HealSkillMasterManager getInstance(){
            return INSTANCE;
        }

        /// <summary> 生成されたHealSkillのリスト </summary>
        private List<HealSkill> dataTable = new List<HealSkill>();
        private Dictionary<int,ActiveSkillProgress> progressTable = new Dictionary<int, ActiveSkillProgress>();

		/// <summary>
		/// idからHealSkillを取得します
		/// </summary>
		/// <returns> 指定されたHealSkill </returns>
		/// <param name="id"> 取得したいHealSkillのId </param>
		public HealSkill getHealSkillFromId(int id){
			foreach(HealSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid HealSkillId");
		}

        public ActiveSkillProgress getHealSkillProgressFromId(int id) {
            return progressTable[id];
		}

		public void addProgress(int worldId) {
            foreach (var builder in dataTable) {
                int id = builder.getId();
                if (ES2.Exists(getLoadPass(id,worldId, "HealSkillProgress"))) {
					var progress = loadSaveData<ActiveSkillProgress>(id, worldId, "HealSkillProgress");
					progressTable[id] = progress;
                }
            }
        }

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
            var skill = new HealSkill(datas);
            dataTable.Add (skill);

			SkillBookDataManager.getInstance().setData(skill);
			progressTable.Add(int.Parse(datas[0]), new ActiveSkillProgress());
		}
        #endregion
    }
}

