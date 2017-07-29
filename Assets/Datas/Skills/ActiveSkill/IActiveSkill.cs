using System;

using Character;
using BattleSystem;

using Extent = Skill.ActiveSkillParameters.Extent;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Skill {
	public interface IActiveSkill : ISkill {
		/// <summary>
		/// このスキルを使用します
		/// </summary>
		/// <param name="actoner"> スキルを使用するIBattleableキャラクター </param>
		/// <param name="task"> スキルを使用したタスク </param>
		void action(IBattleable actioner,BattleTask task);

		/// <summary>
		/// ディレイ秒数を取得します
		/// </summary>
		/// <returns>ディレイするフレーム数</returns>
		float getDelay(IBattleable actioner);

		/// <summary>
		/// ActiveSkillとしての種類を取得します
		/// </summary>
		/// <returns> 種別 </returns>
		ActiveSkillType getActiveSkillType();

		/// <summary>
		/// このスキルが友好的かを取得します
		/// </summary>
		/// <returns><c>true</c>, 友好的スキル, <c>false</c> 非友好的スキル </returns>
		bool isFriendly();
	}
}

