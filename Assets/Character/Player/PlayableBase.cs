using character;
using item;
using parameter;

using System;

namespace character{
	public abstract class PlayableBase : BattleableBase{
		//経験値を表します
		protected int exp = 0;
		//次のレベルアップに必要なexpを表します。
		protected int needExp;

		//対象(武器)を装備します
		public abstract void equipWepon(WeponBase wepon);

		//対象(防具)を装備します
		public abstract void equipArmor(ArmorBase armor);

		//レベルアップし、能力値を成長させます
		public abstract void levelUp();

		//経験値を取得します
		public void addExp(int val){
			if (!(0 < val))
				throw new ArgumentException ("You tried to add wrong value in addExp.");
			exp += val;
		}

		//経験値を返します
		public int getExp(){
			return exp;
		}

		public PlayableBase(BattleableBaseBuilder builder) : base(builder){
		}
	}
}
