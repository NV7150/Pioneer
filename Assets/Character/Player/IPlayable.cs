using character;
using item;
using parameter;

using System;
using System.Collections.Generic;

namespace character{
	public interface IPlayable : IBattleable,IFriendly{
		
		//対象(武器)を装備します
		void equipWepon(WeponBase wepon);

		//対象(防具)を装備します
		void equipArmor(ArmorBase armor);

		//レベルアップし、能力値を成長させます
		void levelUp();

		//経験値を取得します
		void addExp(int val);

		//経験値を返します
		int getExp();

		//器用さを取得します
		int getDex();
	}
}
