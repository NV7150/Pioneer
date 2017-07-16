using System;
using UnityEngine;

using SubBattleAbility = Parameter.CharacterParameters.SubBattleAbility;

namespace Parameter {
	public class SubBattleAbilityBonus : BonusBase{
		private readonly SubBattleAbility BONUS_SUBABILITY;

		public SubBattleAbilityBonus (string name,SubBattleAbility subAbility,float limit,int bonusValue) {
			this.name = name;
			this.BONUS_SUBABILITY = subAbility;
			this.limit = limit;
			this.bonusValue = bonusValue;
		}
		//ボーナス適用対象の能力値を返します
		public SubBattleAbility getBonusAbility(){
			return BONUS_SUBABILITY;
		}
	}
}

