using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

using Character;

namespace AI{
	[InitializeOnLoad]
	public class CowardBuilder : IEnemyAIBuilder {

		private CowardBuilder(){}

		//ゲーム開始時にマネージャに追加します
		static CowardBuilder(){
			EnemyAISummarizingManager.getInstance ().addAi (new CowardBuilder());	
		}

		#region IEnemyAIBuilder implementation
		//CowardAIを生成します
		public IEnemyAI build (IBattleable bal,ActiveSkillSet activeSKills,ReactionSkillSet passiveSkills) {
			return new Coward (bal,activeSKills,passiveSkills);
		}
		public int getId () {
			return Coward.ID;
		}
		#endregion
	}
}
