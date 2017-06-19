﻿using character;
using item;
using parameter;
using skill;

using System;
using System.Collections.Generic;

namespace character{
	public interface IPlayable : IBattleable,IFriendly{
		
		//対象(武器)を装備します。装備不能の場合はfalseを返します
		bool equipWepon(Wepon wepon);

		//対象(武器)を取得します。装備不能の場合はfalseを返します
		Wepon getWepon();

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

		List<ActiveSkill> getActiveSkills ();

		List<IPassiveSkill> getPassiveSKills();
	}
}
