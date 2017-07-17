using System;
using System.Collections;
using System.Collections.Generic;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using SubBattleAbility = Parameter.CharacterParameters.SubBattleAbility;
using Character;
using Parameter;

namespace Skill {
	public abstract class SupportSkillBase {
        /// <summary> ボーナスするBattleAbility </summary>
		protected BattleAbility bonusAbility;
        /// <summary> ボーナスするBattleSubAbility </summary>
		protected SubBattleAbility bonusSubAbility;

        /// <summary> BattleAbilityに対するボーナスかのフラグ </summary>
		protected bool isBonusForAbility;

		/// <summary>
		/// AbilityかSubのどっちかを判断して初期化します
		/// </summary>
		/// <param name="data"> AbilityもしかはSubAblitiyのstringデータ </param>
		protected void setBonusParameter(string data){
			if (Enum.IsDefined (typeof(BattleAbility), data)) {
				bonusAbility = (BattleAbility)Enum.Parse (typeof(BattleAbility), data);
				this.isBonusForAbility = true;
			} else if (Enum.IsDefined (typeof(SubBattleAbility), data)) {
				bonusSubAbility = (SubBattleAbility)Enum.Parse (typeof(SubBattleAbility), data);
				this.isBonusForAbility = false;
			}
		}

        /// <summary>
        /// 対象にボーナスを追加します
        /// </summary>
        /// <param name="targets">適用するIBattleキャラクターのリスト</param>
		protected void setBounsToCharacter(List<IBattleable> targets){
			foreach(IBattleable target in targets){
				if (isBonusForAbility) {
					target.addAbilityBonus (getAbilityBonus ());
				} else {
					target.addAbilityBonus (getSubAbilityBonus());
				}
			}
		}

        /// <summary>
        /// BattleAbilityに対するボーナスの場合のボーナスインスタンスを取得します
        /// </summary>
        /// <returns>ボーナスインスタンス</returns>
		protected abstract BattleAbilityBonus getAbilityBonus ();

        /// <summary>
        /// SubBattleAblityに対するボーナスの場合のボーナスインスタンスを取得します
        /// </summary>
        /// <returns>ボーナスインスタンス</returns>
		protected abstract SubBattleAbilityBonus getSubAbilityBonus ();
	}
}

