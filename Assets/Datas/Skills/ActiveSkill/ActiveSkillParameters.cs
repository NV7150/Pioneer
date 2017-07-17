using System;

namespace Skill{
	public static class ActiveSkillParameters {
		/// <summary>
        /// ActiveSkillの種別
        /// </summary>
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

		/// <summary>
        /// スキルの効果範囲
        /// </summary>
		public enum Extent{
			//単体
			SINGLE,
			//一帯
			AREA,
			//全範囲
			ALL
		}

		/// <summary>
        /// 攻撃スキルの属性
        /// </summary>
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
			PENETRATION
		}

		/// <summary>
        /// 回復スキルの属性
        /// </summary>
		public enum HealSkillAttribute{
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
}

