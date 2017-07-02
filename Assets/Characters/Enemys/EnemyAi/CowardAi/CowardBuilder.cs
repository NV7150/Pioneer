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

		static CowardBuilder(){
			Debug.Log ("Loaded");
			EnemyAISummarizingManager.getInstance ().addAi (new CowardBuilder());	
		}

		#region IEnemyAIBuilder implementation
		public IEnemyAI build (IBattleable bal,ActiveSkillSet activeSKills,PassiveSkillSet passiveSkills) {
			return new Coward (bal,activeSKills,passiveSkills);
		}
		public int getId () {
			return Coward.ID;
		}
		#endregion
	}
}
