using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

namespace MasterData {
	[System.SerializableAttribute]
	public class ActiveSkillMasterManager :MasterDataManagerBase{
		//登録済みのスキルのデータテーブルです
		private static List<ActiveSkill> dataTable = new List<ActiveSkill>();

		private void Awake(){
			Debug.Log ("awaked");
			var activeSkillCSVText = Resources.Load("MasterDatas/ActiveSkillMasterData") as TextAsset;
			constractedBehaviour (activeSkillCSVText);
		}

		//idからActiveSkillを取得します
		public static ActiveSkill getActiveSkillFromId(int id){
			foreach(ActiveSkill skill in dataTable){
				if (skill.getId () == id)
					return skill;
			}
			throw new ArgumentException ("invlit activeSkillId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
			dataTable.Add( new ActiveSkill (datas));
		}

		#endregion
	}
}

