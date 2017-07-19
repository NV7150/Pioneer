using System;
using System.Collections;
using System.Collections.Generic;

using Character;
using Skill;
using BattleSystem;

namespace AI {
	public interface IEnemyAI {
		/// <summary>
        /// 使用するスキルを決定します
        /// </summary>
        /// <returns>使用するスキル</returns>
		IActiveSkill decideSkill();

		/// <summary>
        /// 使用するリアクションスキルを決定します
        /// </summary>
        /// <returns>使用するスキル</returns>
        /// <param name="attacker">攻撃者</param>
        /// <param name="skill">攻撃に使われるAttackSkill</param>
		ReactionSkill decideReaction(IBattleable attacker,AttackSkill skill);

		/// <summary>
        /// スキルを使用する相手を決定します
        /// </summary>
        /// <returns>使用対象のリスト</returns>
        /// <param name="useSkill">使用するスキル</param>
		IBattleable decideSingleTarget (IActiveSkill useSkill);

        FieldPosition decideAreaTarget(IActiveSkill useSkill);

		/// <summary>
        /// moveSkillによる移動量を決定します
        /// </summary>
        /// <returns>移動量</returns>
        /// <param name="useSkill">使用するMoveSkill</param>
		int decideMove(MoveSkill useSkill);

	}
}

