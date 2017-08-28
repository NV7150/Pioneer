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
    public class CowardBuilder : IEnemyAIBuilder {
        private static CowardBuilder INSTANCE = new CowardBuilder(); 

		private CowardBuilder(){}

        public static CowardBuilder getInstance(){
            return INSTANCE;
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
