using System;
using System.Collections;
using System.Collections.Generic;

using Skill;
using AI;
using Parameter;
using MasterData;

namespace AI {
	public class ActiveSkillSet {
		//スキルセットを表すDictionaryです
		private Dictionary<ActiveSkillCategory,ActiveSkill> skillSet = new Dictionary<ActiveSkillCategory, ActiveSkill> ();
		//スキルセットのIDです
		private readonly int ID;
		//スキルセット全体で一番の効果範囲を表します
		private readonly int MAX_RANGE;
		//スキルセットの名前を表します
		private readonly string NAME;

		public ActiveSkillSet (ActiveSkillSetBuilder builder) {
			this.ID = builder.getId ();
			this.NAME = builder.getName ();

			skillSet [ActiveSkillCategory.NORMAL] = builder.getNormalSkill ();
			skillSet [ActiveSkillCategory.CAUTION] = builder.getCautionSkill ();
			skillSet [ActiveSkillCategory.DANGER] = builder.getDangerSkill ();
			skillSet [ActiveSkillCategory.POWER] = builder.getPowerSkill ();
			skillSet [ActiveSkillCategory.FULL_POWER] = builder.getFullPowerSkill ();
			skillSet [ActiveSkillCategory.SUPPORT] = builder.getSupportSkill ();
			skillSet [ActiveSkillCategory.HEAL] = builder.getHealSkill ();
			skillSet [ActiveSkillCategory.MOVE] = builder.getMoveSkill ();

			this.MAX_RANGE = calculateMaxRange ();
		}

		//スキルの中での最大レンジを計算します
		private int calculateMaxRange(){
			int maxRange = 0;
			var keys = skillSet.Keys;
			foreach(ActiveSkillCategory category in keys){
				if (skillSet [category].getRange () > maxRange)
					maxRange = skillSet [category].getRange ();
			}
			return maxRange;
		}

		//スキルセットのIDを取得します
		public int getId(){
			return ID;
		}

		//スキルセットの名前を取得します
		public string getName(){
			return NAME;
		}

		//スキルセットの中で一番の射程を取得します
		public int getMaxRange(){
			return MAX_RANGE;
		}

		//カテゴリからスキルを取得します
		public ActiveSkill getSkillFromSkillCategory(ActiveSkillCategory category){
			return skillSet[category];
		}
	}
}

