using System;
using System.Collections.Generic;

using Character;
using Item;
using Parameter;
using Skill;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;

namespace Character{
    /// <summary>
    /// プレイヤーが操作可能なキャラクターのinterfaceです
    /// </summary>
	public interface IPlayable : IBattleable,IFriendly{
		
		/// <summary>
        /// 武器を装備します
        /// </summary>
        /// <returns><c>true</c>,武器が装備できた時 <c>false</c> 武器が装備できなかった時</returns>
        /// <param name="wepon">装備したい武器</param>
        void equipWeapon(Weapon wepon);

		/// <summary>
        /// 防具を装備します
        /// </summary>
        /// <returns><c>true</c>, 防具が装備できなかった時, <c>false</c> 防具が装備できなかった時</returns>
        /// <param name="armor"> 装備したい防具 </param>
        void equipArmor(Armor armor);

        Weapon getWeapon();

		/// <summary>
        /// 装備している防具を取得します
        /// </summary>
        /// <returns> 装備している防具 </returns>
		Armor getArmor();

        Dictionary<BattleAbility, int> getBattleAbilities();

        Dictionary<FriendlyAbility, int> getFriendlyAbilities();

		/// <summary>
        /// レベルアップし、各能力値の更新を行います
        /// </summary>
		void levelUp();

		/// <summary>
        /// 経験値を追加します
        /// </summary>
        /// <param name="val"> 追加する経験値 </param>
		void addExp(int val);

		/// <summary>
        /// 経験値を取得します
        /// </summary>
        /// <returns> 経験値 </returns>
		int getExp();

		/// <summary>
        /// キャラクターが取得したIActiveSkillのListを取得します
        /// </summary>
        /// <returns>IActionSkillのList</returns>
		List<IActiveSkill> getActiveSkills ();

		/// <summary>
        /// キャラクターが取得したReactionSkillのListを取得します
        /// </summary>
        /// <returns>ReactionSkillのList</returns>
		List<ReactionSkill> getReactionSkills();

		/// <summary>
        /// IActiveSkillを追加します
        /// </summary>
        /// <param name="skill"> 追加するスキル </param>
		void addSkill(IActiveSkill skill);

		/// <summary>
        /// ReactionSkillを追加します
        /// </summary>
        /// <param name="skill"> 追加するスキル </param>
		void addSkill(ReactionSkill skill);
	}
}
