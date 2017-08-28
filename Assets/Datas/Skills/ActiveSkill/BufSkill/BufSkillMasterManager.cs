using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class BufSkillMasterManager : MasterDataManagerBase{
        private static readonly BufSkillMasterManager INSTANCE = new BufSkillMasterManager();

		private BufSkillMasterManager() {
			var csv = (TextAsset)Resources.Load("MasterDatas/BufSkillMasterData");
			constractedBehaviour(csv);
        }

        public static BufSkillMasterManager getInstance(){
            return INSTANCE;
        }

        /// <summary> 登録済みのBufSkillのリストです </summary>
		private List<BufSkill> dataTable = new List<BufSkill>();
        private Dictionary<int, ActiveSkillProgress> progressTable = new Dictionary<int, ActiveSkillProgress>();

        /// <summary>
        /// IDからBufSkillを取得します
        /// </summary>
        /// <returns>指定されたBufSkill</returns>
        /// <param name="id">取得したいBufSkillのID</param>
		public BufSkill getBufSkillFromId(int id){
			foreach(BufSkill skill in dataTable){
				if(skill.getId() == id){
					return skill;
				}
			}
			throw new ArgumentException ("invalid BufSkillId");
		}

        public ActiveSkillProgress getProgressFromId(int id){
            return progressTable[id];
        }

        public void addProgress(int worldId) {
            foreach (var builder in dataTable) {
                int id = builder.getId();
                if (ES2.Exists(getLoadPass(id,worldId, "BufSkillProgress.txt"))) {
                    var progress = loadSaveData<ActiveSkillProgress>(id,worldId, "BufSkillProgress.txt");
					progressTable[id] = progress;
                }
            }
        }


		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
            var skill = new BufSkill(datas);
            dataTable.Add (skill);

			SkillBookDataManager.getInstance().setData(skill);
			progressTable.Add(int.Parse(datas[0]), new ActiveSkillProgress());
		}
        #endregion
    }
}

