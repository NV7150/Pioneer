using System;
using System.Collections;
using System.Collections.Generic;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using SubBattleAbility = Parameter.CharacterParameters.SubBattleAbility;

namespace Parameter {
	public class BonusKeeper {
		/// <summary> BattleAbilityに対するボーナスを格納するdictionary </summary>
		Dictionary<BattleAbility,List<BattleAbilityBonus>> battleAbilityBonusList = new Dictionary<BattleAbility, List<BattleAbilityBonus>> ();
		/// <summary> SubBattleAbilityに対するボーナスを格納するdictionary </summary>
		Dictionary<SubBattleAbility,List<SubBattleAbilityBonus>> subAbilityBonusList = new Dictionary<SubBattleAbility, List<SubBattleAbilityBonus>>();

		public BonusKeeper(){
			var attackAbilities = Enum.GetValues(typeof(BattleAbility));
			foreach(BattleAbility ability in attackAbilities){
				battleAbilityBonusList.Add (ability,new List<BattleAbilityBonus>());
			}

			var subAbilities = Enum.GetValues (typeof(SubBattleAbility));
			foreach(SubBattleAbility ability in subAbilities){
				subAbilityBonusList.Add(ability,new List<SubBattleAbilityBonus>());
			}
		}

		/// <summary>
		/// 与えられたbattleAbilityからボーナスを返します
		/// 引数がBattleAbilityかSubBattleAbilityかでオーバーロードします
		/// </summary>
		/// <returns> 補正値 </returns>
		/// <param name="ability"> 補正したいBattleAbility </param>
		public int getBonus(BattleAbility ability){
			var bonuses = battleAbilityBonusList [ability];
			int sumBonus = 0;
			foreach(BattleAbilityBonus bonus in bonuses){
				sumBonus += bonus.getBonusValue ();
			}
			return sumBonus;
		}

		/// <summary>
		/// 与えられたsubBattleAbilityからボーナスを返します
		/// 引数がBattleAbilityかSubBattleAbilityかでオーバーロードします
		/// </summary>
		/// <returns> 補正値 </returns>
		/// <param name="ability"> 補正したいSubBattleAbility </param>
		public int getBonus(SubBattleAbility ability){
			var bonuses = subAbilityBonusList [ability];
			int sumBonus = 0;
			foreach(SubBattleAbilityBonus bonus in bonuses){
				sumBonus += bonus.getBonusValue ();
			}
			return sumBonus;

		}

		/// <summary>
		/// 与えられたボーナスを追加します
		/// 引数がBattleAbilityBonusかSubBattleAbilityBonusかでオーバーロードします
		/// </summary>
		/// <param name="bonus">追加したいボーナス</param>
		public void setBonus(BattleAbilityBonus bonus) {
			UnityEngine.Debug.Log("setted bonus");
			BattleAbility bonusAbility = bonus.getBonusAbility ();
			battleAbilityBonusList [bonusAbility].Add (bonus);
		}

		/// <summary>
		/// 与えられたボーナスを追加します
		/// 引数がBattleAbilityBonusかSubBattleAbilityBonusかでオーバーロードします
		/// </summary>
		/// <param name="bonus">追加したいボーナス</param>
		public void setBonus(SubBattleAbilityBonus bonus) {
			UnityEngine.Debug.Log("setted bonus");
			SubBattleAbility bonusAbility = bonus.getBonusAbility ();
			subAbilityBonusList [bonusAbility].Add (bonus);
		}

		/// <summary>
		/// 保持しているBonusのlimitを進めます
		/// </summary>
		public void advanceLimit(){

            var battleAbilities = battleAbilityBonusList.Keys;
			foreach(BattleAbility ability in battleAbilities){
                int count = battleAbilityBonusList[ability].Count;
                for (int i = 0; i < count; i++) {
                    BattleAbilityBonus bonus = battleAbilityBonusList[ability][i];
					if (!bonus.nextFrame()){
						battleAbilityBonusList [ability].Remove (bonus);
					}
				}
			}

			var subAbilities = subAbilityBonusList.Keys;
			foreach(SubBattleAbility ability in subAbilities){
                int count = subAbilityBonusList[ability].Count;
                for (int i = 0; i < count;i++){
                    SubBattleAbilityBonus bonus = subAbilityBonusList[ability][i];
					if (!bonus.nextFrame()) {
						subAbilityBonusList [ability].Remove (bonus);
					}
				}
			}
		}

	}
}

