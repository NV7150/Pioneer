using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	public class HealSkillMasterManager : MasterDataManagerBase{
		/// <summary> 生成されたHealSkillのリスト </summary>
		private static List<HealSkill> dataTable = new List<HealSkill>();

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

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
			dataTable.Add (new HealSkill(datas));
		}
		#endregion
	}
}

