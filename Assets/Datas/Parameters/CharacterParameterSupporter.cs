using System;
using System.Collections;
using System.Collections.Generic;

using Faction = Parameter.CharacterParameters.Faction;

namespace Parameter {
	public static class CharacterParameterSupporter {
		/// <summary>
		/// リスト内の勢力に敵対しているものがいるかを返します
		/// </summary>
		/// <returns><c>true</c>, 敵対しているものがある, <c>false</c> ない</returns>
		/// <param name="factions"> 判定したいリスト </param>
		public static bool isThereHostality(List<Faction> factions){
			foreach(Faction gugeFaction in factions){
				foreach(Faction targetFaction in factions){
					if (!isEachFriendly (gugeFaction, targetFaction))
						return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 与えられたFactionが違いに敵対していないかを返します
		/// </summary>
		/// <returns><c>true</c>, 敵対していない , <c>false</c> 敵対している</returns>
		/// <param name="factionOne"> 判定したいfaction </param>
		/// <param name="factionTwo"> 判定したいfaction </param>
		public static bool isEachFriendly(Faction factionOne,Faction factionTwo){
			//かり
			return factionOne == factionTwo;
		}
	}
}

