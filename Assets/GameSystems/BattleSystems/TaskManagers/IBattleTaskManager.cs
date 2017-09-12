using System;

using Skill;
using Character;

namespace BattleSystem {
	public interface IBattleTaskManager {
		/// <summary>
        /// 与えられたターゲットに関連するタスクを削除します
        /// </summary>
        /// <param name="target">削除する対象</param>
		void deleteTaskFromTarget(IBattleable target);

		/// <summary>
        /// リアクションを申請します
        /// </summary>
        /// <param name="attacker">攻撃者</param>
        /// <param name="skill">攻撃者が使用したスキル</param>
		void offerReaction(IBattleable attacker,AttackSkill skill);

		/// <summary>
        /// タスクがあるかを取得しまs
        /// </summary>
        /// <returns><c>true</c>,スキルがある, <c>false</c> スキルがない</returns>
		bool isHavingTask();

		/// <summary> 勝利時の処理です </summary>
		void win();

		/// <summary> 死亡時の処理です </summary>
		void finished();

        void stop();

        void move();
	}
}

