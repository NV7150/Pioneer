using System;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;

namespace Parameter {
	public class BattleAbilityBonus : BonusBase{
		private readonly BattleAbility BONUS_ABILITY;

		public BattleAbilityBonus(string name,BattleAbility ability,float limit,int bonusValue) {
			this.name = name;
			this.BONUS_ABILITY = ability;
			this.limit = limit;
			this.bonusValue = bonusValue;
		}

		//ボーナス適用対象の能力値を返します
		public BattleAbility getBonusAbility(){
			return BONUS_ABILITY;
		}

		public bool hasNextLimit(){
			limit--;
			return (limit > 0);
		}
	}
}

