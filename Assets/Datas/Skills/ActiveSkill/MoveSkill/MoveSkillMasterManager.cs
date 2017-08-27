using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class MoveSkillMasterManager : MasterDataManagerBase{
        private static readonly MoveSkillMasterManager INSTANCE = new MoveSkillMasterManager();

		private MoveSkillMasterManager() {
			var csv = Resources.Load("MasterDatas/MoveSkillMasterData") as TextAsset;
			constractedBehaviour(csv);
        }

        public static MoveSkillMasterManager getInstance(){
            return INSTANCE;
        }

        /// <summary> 登録済みのMoveSkillのリスト </summary>
		private List<MoveSkill> dataTable = new List<MoveSkill>();

        /// <summary>
        /// IDからMoveSkillを取得します
        /// </summary>
        /// <returns> 指定されたMoveSkill </returns>
        /// <param name="id"> 取得したいMoveSkillのID </param>
		public MoveSkill getMoveSkillFromId(int id){
			foreach(MoveSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid MoveSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
            var skill = new MoveSkill(datas);
            dataTable.Add(skill);

            SkillBookDataManager.getInstance().setData(skill);
		}
        #endregion
    }
}

