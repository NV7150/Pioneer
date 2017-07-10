using System;
using System.Collections.Generic;

using Character;
using Item;
using Parameter;
using Skill;

namespace Character{
	public interface IPlayable : IBattleable,IFriendly{
		
		//対象(武器)を装備します。装備不能の場合はfalseを返します
		bool equipWepon(Wepon wepon);

		//対象(防具)を装備します
		bool equipArmor(Armor armor);

		//対象(防具)を取得します
		Armor getArmor();

		//レベルアップし、能力値を成長させます
		void levelUp();

		//経験値を取得します
		void addExp(int val);

		//経験値を返します
		int getExp();

		//器用さを取得します
		int getDex();

		//取得済みの能動スキルを取得します
		List<IActiveSkill> getActiveSkills ();

		//取得済みの受動スキルを取得します
		List<ReactionSkill> getReactionSKills();

		//能動スキルを獲得します。オーバーロードされます。
		void addSkill(IActiveSkill skill);

		//受動スキルを取得します。オーバーロードされます。
		void addSkill(ReactionSkill skill);
	}
}
