using System;
using System.Collections;
using System.Collections.Generic;

using Skill;
using AI;
using Parameter;
using MasterData;
using Character;

namespace AI {
	public class ActiveSkillSet {
		//スキルセットを表すDictionaryです
		private Dictionary<ActiveSkillCategory,IActiveSkill> skillSet = new Dictionary<ActiveSkillCategory, IActiveSkill> ();
		//スキルセットのIDです
		private readonly int ID;
		//スキルセット全体で一番の効果範囲を表します
		private int MAX_RANGE;
		//スキルセットの名前を表します
		private readonly string NAME;

		public ActiveSkillSet (ActiveSkillSetBuilder builder,IBattleable user) {
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

			calculateMaxRange (user);
		}

		//スキルの中での最大レンジを計算します
		private int calculateMaxRange(IBattleable user){
			int maxRange = 0;
			var keys = skillSet.Keys;
			foreach(ActiveSkillCategory category in keys){
				if (ActiveSkillSupporter.needsTarget (skillSet [category])) {
					int skillRange = ActiveSkillSupporter.searchRange (skillSet[category],user);
					if (skillRange > maxRange)
						maxRange = skillRange;
				}
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
		public IActiveSkill getSkillFromSkillCategory(ActiveSkillCategory category){
			return skillSet[category];
		}
	}
}

