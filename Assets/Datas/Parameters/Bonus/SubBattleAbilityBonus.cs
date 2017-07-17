using System;
using UnityEngine;

using SubBattleAbility = Parameter.CharacterParameters.SubBattleAbility;

namespace Parameter {
    /// <summary>
    /// SubBattleAbiltiyに対するボーナス
    /// </summary>
	public class SubBattleAbilityBonus : BonusBase{
        /// <summary> ボーナスするSubBattleAbility </summary>
		private readonly SubBattleAbility BONUS_SUBABILITY;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">ボーナス名</param>
        /// <param name="subAbility">適用するSubBattleAbitliy</param>
        /// <param name="limit">効果時間</param>
        /// <param name="bonusValue">ボーナス量</param>
		public SubBattleAbilityBonus (string name,SubBattleAbility subAbility,float limit,int bonusValue) {
			this.name = name;
			this.BONUS_SUBABILITY = subAbility;
			this.limit = limit;
			this.bonusValue = bonusValue;
		}

		/// <summary>
        /// ボーナスを適用するSubBattleAbilityを取得します
        /// </summary>
        /// <returns>適用するSubBattleAbility</returns>
		public SubBattleAbility getBonusAbility(){
			return BONUS_SUBABILITY;
		}
	}
}

