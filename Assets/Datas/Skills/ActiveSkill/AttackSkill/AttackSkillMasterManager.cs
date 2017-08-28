using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class AttackSkillMasterManager : MasterDataManagerBase {
        private static readonly AttackSkillMasterManager INSTANCE = new AttackSkillMasterManager();
		private AttackSkillMasterManager() {
			var csv = Resources.Load("MasterDatas/AttackSkillMasterData") as TextAsset;
			constractedBehaviour(csv);
        }

        public static AttackSkillMasterManager getInstance(){
            return INSTANCE;
        }

		/// <summary> 生成済みのAttackSkillのリスト </summary>
		private List<AttackSkill> dataTable = new List<AttackSkill>();
        private Dictionary<int,ActiveAttackSkillProgress> progressTable = new Dictionary<int, ActiveAttackSkillProgress>();

		/// <summary>
		/// IDからAttackSkillを取得します
		/// </summary>
		/// <returns> 結果のAttackSkill </returns>
		/// <param name="id"> 取得したいスキルのID </param>
		public AttackSkill getAttackSkillFromId(int id){
			foreach(AttackSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid AttackSkillId");
		}

        public ActiveSkillProgress getAttackSkillProgressFromId(int id) {
            return progressTable[id];
		}

        public void addProgress(int worldId){
            foreach(var skill in dataTable){
                int id = skill.getId();

                if (ES2.Exists(getLoadPass(id,worldId, "AttackSkillProgress.txt"))) {
                    var progress = loadSaveData<ActiveAttackSkillProgress>(id,worldId, "AttackSkillProgress.txt");
					skill.addProgress(progress);
					progressTable[id] =  progress;
				}
            }
        }

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
            var skill = new AttackSkill(datas);
            dataTable.Add (skill);

			SkillBookDataManager.getInstance().setData(skill);
            progressTable.Add(int.Parse(datas[0]), new ActiveAttackSkillProgress());
		}

        #endregion
    }
}

