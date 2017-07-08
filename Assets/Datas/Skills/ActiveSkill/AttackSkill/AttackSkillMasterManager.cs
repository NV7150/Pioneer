using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class AttackSkillMasterManager : MasterDataManagerBase {
		/// <summary> 生成済みのAttackSkillのリスト </summary>
		private static List<AttackSkill> dataTable = new List<AttackSkill>();

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

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
			dataTable.Add (new AttackSkill(datas));
		}

		#endregion
	}
}

