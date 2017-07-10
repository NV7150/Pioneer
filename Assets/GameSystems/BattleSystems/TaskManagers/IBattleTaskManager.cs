using System;

using Skill;
using Character;

namespace BattleSystem {
	public interface IBattleTaskManager {
		//与えられたキャラクターが対象のスキルを削除します
		void deleteTaskFromTarget(IBattleable target);

		//リアクションを申請します
		void offerReaction(IBattleable attacker,AttackSkill skill);

		//タスクがあるかを返します
		bool isHavingTask();

		/// <summary> 勝利時の処理です </summary>
		void win();

		/// <summary> 死亡時の処理です </summary>
		void finished();
	}
}

