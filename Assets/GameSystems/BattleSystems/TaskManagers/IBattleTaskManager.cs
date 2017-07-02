using System;

using Skill;
using Character;

namespace BattleSystem {
	public interface IBattleTaskManager {
		//与えられたキャラクターが対象のスキルを削除します
		void deleteTaskFromTarget(IBattleable target);

		//リアクションを申請します
		void offerReaction(IBattleable attacker,ActiveSkill skill);

		//タスクがあるかを返します
		bool isHavingTask();
	}
}

