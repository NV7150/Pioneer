using System;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;

namespace Parameter {
    /// <summary>
    /// BattleAbilityに対するボーナス
    /// </summary>
	public class BattleAbilityBonus : BonusBase{
        /// <summary>
        /// ボーナスがかかるBattleAbility
        /// </summary>
		private readonly BattleAbility BONUS_ABILITY;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">ボーナス名</param>
        /// <param name="ability">ボーナスするBattleAbiltiy</param>
        /// <param name="limit">ボーナスの効果時間</param>
        /// <param name="bonusValue">ボーナス量</param>
		public BattleAbilityBonus(string name,BattleAbility ability,float limit,int bonusValue) {
			this.name = name;
			this.BONUS_ABILITY = ability;
			this.limit = limit;
			this.bonusValue = bonusValue;
		}

		/// <summary>
        /// ボーナスを適用するBattleAbility
        /// </summary>
        /// <returns>適用するBattleAbility</returns>
		public BattleAbility getBonusAbility(){
			return BONUS_ABILITY;
		}
	}
}

