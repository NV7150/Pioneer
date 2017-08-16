using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class AttackSkillMasterManager : MasterDataManagerBase {
		/// <summary> 生成済みのAttackSkillのリスト </summary>
		private static List<AttackSkill> dataTable = new List<AttackSkill>();
        private static Dictionary<int,ActiveAttackSkillProgress> progressTable = new Dictionary<int, ActiveAttackSkillProgress>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/AttackSkillMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		/// <summary>
		/// IDからAttackSkillを取得します
		/// </summary>
		/// <returns> 結果のAttackSkill </returns>
		/// <param name="id"> 取得したいスキルのID </param>
		public static AttackSkill getAttackSkillFromId(int id){
			foreach(AttackSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid AttackSkillId");
		}

        public static ActiveSkillProgress getAttackSkillProgressFromId(int id) {
            return progressTable[id];
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
            var skill = new AttackSkill(datas);
            dataTable.Add (skill);

            int id = int.Parse(datas[0]);
            if (ES2.Exists(getLoadPass(id,"AttackSkillProgress.txt"))) {
                var progress = loadSaveData<ActiveAttackSkillProgress>(id, "AttackSkillProgress.txt");
                skill.addProgress(progress);
                progressTable.Add(id,progress);
            }else{
                progressTable.Add(id,new ActiveAttackSkillProgress());
            }
		}

        #endregion
    }
}

