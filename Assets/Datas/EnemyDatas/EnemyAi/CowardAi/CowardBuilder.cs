using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

using Character;
using Skill;

namespace AI{
    /// <summary>
    /// cowardを生成するビルダー
    /// </summary>
	[InitializeOnLoad]
	public class CowardBuilder : IEnemyAIBuilder {

		private CowardBuilder(){}

		/// <summary>
        /// ゲーム開始時に自身をEnemyAiSummarizingManagerに登録します
        /// </summary>
		static CowardBuilder(){
			EnemyAISummarizingManager.getInstance ().addAi (new CowardBuilder());	
		}

		#region IEnemyAIBuilder implementation
		public IEnemyAI build (IBattleable bal,ActiveSkillSet activeSKills,ReactionSkillSet passiveSkills) {
			return new Coward (bal,activeSKills,passiveSkills);
		}
		public int getId () {
			return Coward.ID;
		}
		#endregion
	}
}
