using System;

namespace Skill{
	public static class ActiveSkillParameters {
		//ActiveSkillの種別です
		public enum ActiveSkillType{
			//攻撃
			ATTACK,
			//移動
			MOVE,
			//回復
			HEAL,
			//バフ
			BUF,
			//デバフ
			DEBUF
		}

		//スキルの効果範囲です
		public enum Extent{
			//単体
			SINGLE,
			//一帯
			AREA,
			//全範囲
			ALL,
			//なし
			NONE
		}

		//攻撃スキルの属性です
		public enum AttackSkillAttribute{
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
			PENETRATION,
			//なし
			NONE
		}

		//回復スキルの属性です
		public enum HealSkillAttribute{
			//HP回復
			HP_HEAL,
			//MP回復
			MP_HEAL,
			//両方
			BOTH,
			//蘇生
			RESURRECTITION,
			//なし
			NONE
		}
	}
}

