using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parameter{
    /// <summary>
    /// キャラクターのパラメータのEnum
    /// </summary>
	public static class CharacterParameters{
		/// <summary>
        /// 戦闘に使用する能力値
        /// </summary>
		public enum BattleAbility{
			//白兵戦闘力
			MFT,
			//遠戦闘力
			FFT,
			//魔力
			MGP,
			//敏捷
			AGI,
			//体力
			PHY
		}

        /// <summary>
        /// 戦闘に関係ない能力値
        /// </summary>
		public enum FriendlyAbility{
			//話術
			SPC,
			//器用
			DEX
		}

		/// <summary>
        /// BattleAbilityに依存して設定される能力値
        /// </summary>
		public enum SubBattleAbility{
			//攻撃力
			ATK,
			//防御力
			DEF,
			//回避力
			DODGE
		}

		/// <summary>
        /// 所属している派閥
        /// </summary>
		public enum Faction {
			PLAYER,
            ENEMY
		}

        public enum FriendlyCharacterType {
            CITIZEN,
            MERCHANT,
            PLAYABLE,
            CLIENT
        }
    }
}
