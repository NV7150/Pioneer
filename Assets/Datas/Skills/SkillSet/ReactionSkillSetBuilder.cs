using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;
using AI;

using ReactionSkillType = Skill.ReactionSkillParameters.ReactionSkillType;

namespace MasterData{
	[SerializableAttribute]
	public class ReactionSkillSetBuilder{
		//プロパティです
		private int 
			id,
			dodgeSkillId,
			guardSkillId;
		private string name;

		/// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
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

		public Dictionary<ReactionSkillType,ReactionSkill> getSet(){
			Dictionary<ReactionSkillType,ReactionSkill> skills = new Dictionary<ReactionSkillType, ReactionSkill> ();
            skills.Add (ReactionSkillType.DODGE,ReactionSkillMasterManager.getInstance().getReactionSkillFromId(dodgeSkillId));
            skills.Add (ReactionSkillType.GUARD, ReactionSkillMasterManager.getInstance().getReactionSkillFromId (guardSkillId));
			return skills;
		}

		/// <summary>
        /// ReactionSkillSetを生成します
        /// </summary>
        /// <returns>生成したReactionSkillSet</returns>
		public ReactionSkillSet build(){
			return new ReactionSkillSet (this);
		}
	}
}
