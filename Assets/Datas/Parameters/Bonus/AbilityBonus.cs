using System;

using Ability = Parameter.CharacterParameters.Ability;

namespace Parameter {
	public class AbilityBonus : BonusBase{
		private readonly Ability BONUS_ABILITY;

		public AbilityBonus(string name,Ability ability,int limit,int bonusValue) {
			this.name = name;
			this.BONUS_ABILITY = ability;
			this.limit = limit;
			this.bonusValue = bonusValue;
		}

		//ボーナス適用対象の能力値を返します
		public Ability getBonusAbility(){
			return BONUS_ABILITY;
		}
	}
}

