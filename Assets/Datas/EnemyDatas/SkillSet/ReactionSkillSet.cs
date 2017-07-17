using System;
using System.Collections;
using System.Collections.Generic;

using Skill;
using AI;
using MasterData;

using ReactionSkillCategory = Skill.ReactionSkillParameters.ReactionSkillCategory;

namespace AI {
	public class ReactionSkillSet {

		/// <summary> スキルセットのID </summary>
		private readonly int ID;
		/// <summary> スキルセット名 </summary>
		private readonly string NAME;
		/// <summary> スキルセットを表すDictionary </summary>
		private Dictionary<ReactionSkillCategory,ReactionSkill> skillSet;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="builder">ビルダー</param>
		public ReactionSkillSet (ReactionSkillSetBuilder builder) {
			this.ID = builder.getId ();
			this.NAME = builder.getName ();
			this.skillSet = builder.getSet ();
		}

		/// <summary>
        /// IDを取得します
        /// </summary>
        /// <returns>スキルセットのID</returns>
		public int getId(){
			return ID;
		}

		/// <summary>
        /// スキルセット名を取得します
        /// </summary>
        /// <returns>スキルセット名</returns>
		public string getName(){
			return NAME;
		}

		/// <summary>
		/// カテゴリからReactionSkillを取得します
		/// </summary>
		/// <returns> 指定されたReactionSkill </returns>
		/// <param name="category"> 取得したいスキルのカテゴリ </param>
		public ReactionSkill getReactionSkillFromCategory(ReactionSkillCategory category){
			return skillSet [category];
		}
	}
}

