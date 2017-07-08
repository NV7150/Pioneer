using System;
using System.Collections;
using System.Collections.Generic;

using Ability = Parameter.CharacterParameters.Ability;
using SubAbility = Parameter.CharacterParameters.SubAbility;
using Character;
using Parameter;

namespace Skill {
	public abstract class SupportSkillBase {

		protected Ability bonusAbility;
		protected SubAbility bonusSubAbility;

		protected bool isBonusForAbility;

		/// <summary>
		/// AbilityかSubのどっちかを判断して初期化します
		/// </summary>
		/// <param name="data"> AbilityもしかはSubAblitiyのstringデータ </param>
		protected void setBonusParameter(string data){
			if (Enum.IsDefined (typeof(Ability), data)) {
				bonusAbility = (Ability)Enum.Parse (typeof(Ability), data);
				this.isBonusForAbility = true;
			} else if (Enum.IsDefined (typeof(SubAbility), data)) {
				bonusSubAbility = (SubAbility)Enum.Parse (typeof(SubAbility), data);
				this.isBonusForAbility = false;
			}
		}

		protected void setBounsToCharacter(List<IBattleable> targets){
			foreach(IBattleable target in targets){
				if (isBonusForAbility) {
					target.addAbilityBonus (getAbilityBonus ());
				} else {
					target.addSubAbilityBonus (getSubAbilityBonus());
				}
			}
		}

		protected abstract AbilityBonus getAbilityBonus ();

		protected abstract SubAbilityBonus getSubAbilityBonus ();
	}
}

