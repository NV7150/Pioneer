using System;
using System.Collections.Generic;

using Character;
using Item;
using Parameter;
using Skill;

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
		bool equipWepon(Wepon wepon);

		/// <summary>
        /// 防具を装備します
        /// </summary>
        /// <returns><c>true</c>, 防具が装備できなかった時, <c>false</c> 防具が装備できなかった時</returns>
        /// <param name="armor"> 装備したい防具 </param>
		bool equipArmor(Armor armor);

		/// <summary>
        /// 装備している防具を取得します
        /// </summary>
        /// <returns> 装備している防具 </returns>
		Armor getArmor();

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
        /// 器用さを取得します
        /// </summary>
        /// <returns> 器用さ </returns>
		int getDex();

		/// <summary>
        /// キャラクターが取得したIActiveSkillのListを取得します
        /// </summary>
        /// <returns>IActionSkillのList</returns>
		List<IActiveSkill> getActiveSkills ();

		/// <summary>
        /// キャラクターが取得したReactionSkillのListを取得します
        /// </summary>
        /// <returns>ReactionSkillのList</returns>
		List<ReactionSkill> getReactionSKills();

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
