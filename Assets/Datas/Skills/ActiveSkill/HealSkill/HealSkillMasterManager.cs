using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class HealSkillMasterManager : MasterDataManagerBase{
        /// <summary> 生成されたHealSkillのリスト </summary>
        private static List<HealSkill> dataTable = new List<HealSkill>();
        private static Dictionary<int,ActiveSkillProgress> progressTable = new Dictionary<int, ActiveSkillProgress>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/HealSkillMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		/// <summary>
		/// idからHealSkillを取得します
		/// </summary>
		/// <returns> 指定されたHealSkill </returns>
		/// <param name="id"> 取得したいHealSkillのId </param>
		public static HealSkill getHealSkillFromId(int id){
			foreach(HealSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid HealSkillId");
		}

        public static ActiveSkillProgress getHealSkillProgressFromId(int id) {
            return progressTable[id];
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
            var builder = new HealSkill(datas);
            dataTable.Add (builder);
            int id = int.Parse(datas[0]);
            if (ES2.Exists(getLoadPass(id,"HealSkillProgress"))) {
                var progress = loadSaveData<ActiveSkillProgress>(int.Parse(datas[0]), "HealSkillProgress");
                progressTable.Add(id, progress);
            }else{
                progressTable.Add(id,new ActiveSkillProgress());
            }
		}
        #endregion
    }
}

