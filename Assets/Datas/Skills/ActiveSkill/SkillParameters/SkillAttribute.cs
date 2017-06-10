using System;

namespace skill {
	public enum SkillAttribute{
		//物理
		PHYSICAL,
		//魔法（火）
		FIRE,
		//魔法（水）
		WATER,
		//魔法（風）
		WIND,
		//魔法（土）
		EARTH,
		//魔法（神聖）
		HOLY,
		//魔法（無）
		NEUTRAL,
		//貫通
		PENETRATION
	}
	public enum HealAttribute{
		//HP回復
		HP_HEAL,
		//MP回復
		MP_HEAL,
		//両方
		BOTH,
		//蘇生
		RESURRECTITION
	}
}

