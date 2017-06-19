using System;

namespace Skill {
	public enum SkillType{
		//攻撃もしくは回復等
		ACTION,
		//移動
		MOVE,
		//移動攻撃
		ACTION_AND_MOVE,
		//なし
		NONE
	}

	public enum ActType{
		//攻撃
		ATTACK,
		//回復
		HEAL,
		//どっちも
		BOTH,
		//なし
		NONE
	}
}

