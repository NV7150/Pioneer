using System;
using System.Collections;
using System.Collections.Generic;

using Character;
using Skill;

namespace AI {
    /// <summary>
    /// EnemyAIBuilderを集約し、求めに応じてAIインスタンスを設定します
    /// </summary>
	public class EnemyAISummarizingManager {
		/// <summary> 唯一のAI </summary>
		private static readonly EnemyAISummarizingManager INSTANCE = new EnemyAISummarizingManager();
		/// <summary> 追加されたAIBuilder </summary>
		private List<IEnemyAIBuilder> summarizingAI = new List<IEnemyAIBuilder>();

		/// <summary>
        /// シングルトンです
        /// </summary>
		private EnemyAISummarizingManager () {
            summarizingAI.Add(CowardBuilder.getInstance());
        }

		/// <summary>
        /// 唯一のインスタンスを取得します
        /// </summary>
        /// <returns>唯一のインスタンス</returns>
		public static EnemyAISummarizingManager getInstance(){
			return INSTANCE;
		}

		/// <summary>
        /// AIBuilderを追加します
        /// </summary>
        /// <param name="builder">追加するAIBuiler</param>
		public void addAi(IEnemyAIBuilder builder){
			summarizingAI.Add (builder);
		}

		/// <summary>
        /// AIをidから取得します
        /// </summary>
        /// <returns>オーダーされたAI</returns>
        /// <param name="id">取得したいAIのID</param>
        /// <param name="user">AIを設定したいIBattleableオブジェクト</param>
        /// <param name="activeSkills">userのActiveSkillSet.</param>
        /// <param name="reactionSkills">userのReactionSkillSet.</param>
        public IEnemyAI getAiFromId(int id,IBattleable user,ActiveSkillSet activeSkills,ReactionSkillSet reactionSkills){
			foreach(IEnemyAIBuilder builder in summarizingAI){
				if (builder.getId() == id)
					return builder.build(user,activeSkills,reactionSkills);
			}
			throw new ArgumentException ("invalid aiId");
		}
	}
}

