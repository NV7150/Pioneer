using System;

using Skill;
using Character;

namespace BattleSystem {
	public interface IBattleTaskManager {
		BattleTask getTask();
		void deleteTaskFromTarget(IBattleable target);
		void offerPassive();
		PassiveSkill getPassive(IBattleable attacker,ActiveSkill skill);
		bool isHavingTask();
	}
}

