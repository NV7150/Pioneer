using System;
using System.Collections;
using System.Collections.Generic;

using Character;
using Skill;
using BattleSystem;

namespace AI {
	public interface IEnemyAI {

		//与えられたデータを元に、使うスキルを判断します
		IActiveSkill decideSkill();

		//与えられたデータを元に、リアクションを決定します
		ReactionSkill decideReaction(IBattleable attacker,AttackSkill skill);

		//与えられたデータを元に、攻撃する敵を判断します
		List<IBattleable> decideTarget (List<IBattleable> targets,IActiveSkill useSkill);

		//与えられたデータを元に、移動量を決定します
		int decideMove(MoveSkill useSkill);

	}
}

