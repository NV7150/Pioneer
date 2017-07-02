using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;
using AI;

namespace MasterData{
	[SerializableAttribute]
	public class PassiveSkillSetBuilder{
		private int 
			id,
			dodgeSkillId,
			guardSkillId;
		private string name;

		public PassiveSkillSetBuilder(string[] datas){
			id = int.Parse (datas[0]);
			name = datas[1];
			dodgeSkillId = int.Parse (datas [2]);
			guardSkillId = int.Parse (datas [3]);
		}

		public int getId(){
			return id;
		}

		public string getName(){
			return name;
		}

		public Dictionary<PassiveSkillCategory,PassiveSkill> getSet(){
			Dictionary<PassiveSkillCategory,PassiveSkill> skills = new Dictionary<PassiveSkillCategory, PassiveSkill> ();
			skills.Add (PassiveSkillCategory.DODGE,PassiveSkillMasterManager.getPassiveSkillFromId(dodgeSkillId));
			skills.Add (PassiveSkillCategory.GUARD, PassiveSkillMasterManager.getPassiveSkillFromId (guardSkillId));
			return skills;
		}

		public PassiveSkillSet build(){
			return new PassiveSkillSet (this);
		}
	}
}
