using System;

using SubAbility = Parameter.CharacterParameters.SubAbility;

namespace Parameter {
	public class SubAbilityBonus : BonusBase{
		private readonly SubAbility BONUS_SUBABILITY;

		public SubAbilityBonus (string name,SubAbility subAbility,int limit,int bonusValue) {
			this.name = name;
			this.BONUS_SUBABILITY = subAbility;
			this.limit = limit;
			this.bonusValue = bonusValue;
		}
		//ボーナス適用対象の能力値を返します
		public SubAbility getBonusAbility(){
			return BONUS_SUBABILITY;
		}
	}
}

