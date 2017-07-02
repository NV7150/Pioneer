using System;
using System.Collections;
using System.Collections.Generic;

using Skill;
using AI;
using MasterData;

using ReactionSkillCategory = Skill.ReactionSkillParameters.ReactionSkillCategory;

namespace AI {
	public class ReactionSkillSet {

		//スキルセットのIDです
		private readonly int ID;
		//スキルセットの名前です
		private readonly string NAME;
		//スキルセットを表すDictionaryです
		private Dictionary<ReactionSkillCategory,ReactionSkill> skillSet;

		public ReactionSkillSet (ReactionSkillSetBuilder builder) {
			this.ID = builder.getId ();
			this.NAME = builder.getName ();
			this.skillSet = builder.getSet ();
		}

		//IDを取得します
		public int getId(){
			return ID;
		}

		//名前を取得します
		public string getName(){
			return NAME;
		}

		//カテゴリからスキルを取得します
		public ReactionSkill getReactionSkillFromCategory(ReactionSkillCategory category){
			return skillSet [category];
		}
	}
}

