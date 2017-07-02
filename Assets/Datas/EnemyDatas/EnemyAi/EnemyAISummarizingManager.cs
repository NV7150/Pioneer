using System;
using System.Collections;
using System.Collections.Generic;

using AI;
using Character;

namespace AI {
	public class EnemyAISummarizingManager {
		//唯一のインスタンスです
		private static readonly EnemyAISummarizingManager INSTANCE = new EnemyAISummarizingManager();
		//追加されたAIです
		private List<IEnemyAIBuilder> summarizingAI = new List<IEnemyAIBuilder>();

		//シングルトンのため、コンストラクタはないです
		private EnemyAISummarizingManager () {}

		//唯一のインスタンスを取得します
		public static EnemyAISummarizingManager getInstance(){
			return INSTANCE;
		}

		//AIを追加します
		public void addAi(IEnemyAIBuilder builder){
			summarizingAI.Add (builder);
		}

		//AIをIDか取得します
		public IEnemyAI getAiFromId(int id,IBattleable bal,ActiveSkillSet activeSkills,ReactionSkillSet passiveSkills){
			foreach(IEnemyAIBuilder builder in summarizingAI){
				if (builder.getId() == id)
					return builder.build(bal,activeSkills,passiveSkills);
			}
			throw new ArgumentException ("invalid aiId");
		}
	}
}

