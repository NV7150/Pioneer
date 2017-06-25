using System;
using System.Collections;
using System.Collections.Generic;

using AI;
using Character;

namespace AI {
	public class EnemyAISummarizingManager {
		private static readonly EnemyAISummarizingManager INSTANCE = new EnemyAISummarizingManager();
		private List<IEnemyAIBuilder> summarizingAI = new List<IEnemyAIBuilder>();

		private EnemyAISummarizingManager () {}

		public static EnemyAISummarizingManager getInstance(){
			return INSTANCE;
		}

		public void addAi(IEnemyAIBuilder builder){
			summarizingAI.Add (builder);
		}

		public IEnemyAI getAiFromId(int id,IBattleable bal){
			foreach(IEnemyAIBuilder builder in summarizingAI){
				if (builder.getId() == id)
					return builder.build(bal);
			}
			throw new ArgumentException ("invlid aiId");
		}
	}
}

