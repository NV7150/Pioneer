using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace parameter{
	public class Civil : Job{
		public string getName (){
			return "一般市民";
		}

		/*能力値
		 * 白兵戦闘力:C
		 * 遠距離戦闘力:C
		 * 敏捷性:C
		 * 体力:C
		 * 話術:B
		 * 器用:B
		 * 魔力:C
		*/
		public Dictionary<Ability,int> defaultSetting(){
			Dictionary<Ability,int> setting = new Dictionary<Ability,int> ();
			setting [Ability.MFT] = 1;
			setting [Ability.FFT] = 1;
			setting [Ability.AGI] = 1;
			setting [Ability.PHY] = 1;
			setting [Ability.SPC] = 2;
			setting [Ability.DEX] = 2;
			setting [Ability.MGP] = 1;

			return setting;
		}
	}
}
