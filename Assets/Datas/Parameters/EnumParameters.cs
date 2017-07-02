﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parameter{
	public static class EnumParameters {
		//能力値を表します
		public enum Ability{
			//最大HP
			HP,
			//最大MP
			MP,
			//白兵戦闘力
			MFT,
			//遠戦闘力
			FFT,
			//魔力
			MGP,
			//敏捷
			AGI,
			//体力
			PHY,
			//話術
			SPC,
			//器用
			DEX,
			//レベル
			LV
		} 

		//所属する勢力を表します
		public enum Faction {
			PLAYER,ENEMY
		}
	}
}
