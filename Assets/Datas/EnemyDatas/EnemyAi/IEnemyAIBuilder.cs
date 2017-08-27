using Character;
using Skill;

namespace AI{
	public interface IEnemyAIBuilder {
		/// <summary>
        /// AIを生成します
        /// </summary>
        /// <returns>生成したAI</returns>
        /// <param name="user">AIを設定するIBattleableキャラクター</param>
        /// <param name="activeSkills">userが持つActiveSkillSet</param>
        /// <param name="reactionSkills">userが持つReactionSkillSet</param>
        IEnemyAI build(IBattleable user,ActiveSkillSet activeSkills,ReactionSkillSet reactionSkills);

		/// <summary>
        /// 保有するAIのID
        /// </summary>
        /// <returns>AIのID</returns>
		int getId();
	}
}
