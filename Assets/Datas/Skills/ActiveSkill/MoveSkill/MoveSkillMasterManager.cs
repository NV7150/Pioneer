using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class MoveSkillMasterManager : MasterDataManagerBase{
        /// <summary> 登録済みのMoveSkillのリスト </summary>
		private static List<MoveSkill> dataTable = new List<MoveSkill>();

		void Awake(){
			var csv = Resources.Load ("MasterDatas/MoveSkillMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

        /// <summary>
        /// IDからMoveSkillを取得します
        /// </summary>
        /// <returns> 指定されたMoveSkill </returns>
        /// <param name="id"> 取得したいMoveSkillのID </param>
		public static MoveSkill getMoveSkillFromId(int id){
			foreach(MoveSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invalid MoveSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
			dataTable.Add(new MoveSkill(datas));
		}
		#endregion
	}
}

