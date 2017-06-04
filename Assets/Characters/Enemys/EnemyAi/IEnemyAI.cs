﻿using System;
using System.Collections;
using System.Collections.Generic;

using character;
using skill;
using battleSystem;

namespace AI {
	public interface IEnemyAI {
		//与えられたデータを元に、行動を決めます
		BattleCommand decideCommand();

		//与えられたデータを元に、使うスキルを判断します
		IActiveSkill decideSkill();

		//スキルテーブルを設定します
		void setSkillTable(Dictionary<SkillCategory,IActiveSkill> skillTable);

		//与えられたデータを元に、攻撃する敵を判断します
		List<IBattleable> decideTarget (List<IBattleable> targets,IActiveSkill useSkill);

		//与えられたデータを元に、移動量を決定します
		int getMove();

		void setNowHp(int hp);

		void setNowMp(int mp);

		void setNowPosition(FiealdPosition pos);
	}

	public enum SkillCategory{
		NORMAL,CAUTION,DANGER,POWER,FULL_POWER,SUPPORT,HEAL
	}
}
