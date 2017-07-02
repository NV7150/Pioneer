using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;
using AI;

namespace MasterData{
	[SerializableAttribute]
	public class ReactionSkillSetBuilder{
		//プロパティです
		private int 
			id,
			dodgeSkillId,
			guardSkillId;
		private string name;

		//csvによるstring配列から初期化します
		public ReactionSkillSetBuilder(string[] datas){
			id = int.Parse (datas[0]);
			name = datas[1];
			dodgeSkillId = int.Parse (datas [2]);
			guardSkillId = int.Parse (datas [3]);
		}

		//各値のgetterです

		public int getId(){
			return id;
		}

		public string getName(){
			return name;
		}

		public Dictionary<ReactionSkillCategory,ReactionSkill> getSet(){
			Dictionary<ReactionSkillCategory,ReactionSkill> skills = new Dictionary<ReactionSkillCategory, ReactionSkill> ();
			skills.Add (ReactionSkillCategory.DODGE,ReactionSkillMasterManager.getReactionSkillFromId(dodgeSkillId));
			skills.Add (ReactionSkillCategory.GUARD, ReactionSkillMasterManager.getReactionSkillFromId (guardSkillId));
			return skills;
		}

		//PassiveSkillSetを取得します
		public ReactionSkillSet build(){
			return new ReactionSkillSet (this);
		}
	}
}
