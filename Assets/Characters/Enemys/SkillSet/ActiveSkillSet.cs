using System;
using System.Collections;
using System.Collections.Generic;

using skill;
using AI;
using parameter;
using masterdata;

namespace AI {
	public class ActiveSkillSet {
		private Dictionary<SkillCategory,ActiveSkill> skillSet = new Dictionary<SkillCategory, ActiveSkill> ();
		private int id;
		private int	maxRange;
		private string name;

		public ActiveSkillSet (ActiveSkillSetBuilder builder) {
			this.id = builder.getId ();
			this.name = builder.getName ();

			skillSet [SkillCategory.NORMAL] = builder.getNormalSkill ();
			skillSet [SkillCategory.CAUTION] = builder.getCautionSkill ();
			skillSet [SkillCategory.DANGER] = builder.getDangerSkill ();
			skillSet [SkillCategory.POWER] = builder.getPowerSkill ();
			skillSet [SkillCategory.FULL_POWER] = builder.getFullPowerSkill ();
			skillSet [SkillCategory.SUPPORT] = builder.getSupportSkill ();
			skillSet [SkillCategory.HEAL] = builder.getHealSkill ();
			skillSet [SkillCategory.MOVE] = builder.getMoveSkill ();

			calculateMaxRange ();
		}

		private int calculateMaxRange(){
			int maxRange = 0;
			foreach(SkillCategory category in skillSet.Keys){
				if (skillSet [category].getRange () > maxRange)
					maxRange = skillSet [category].getRange ();
			}
			return maxRange;
		}
			
		public int getId(){
			return id;
		}

		public string getName(){
			return name;
		}

		public int getMaxRange(){
			return maxRange;
		}

		public ActiveSkill getSkillFromSkillCategory(SkillCategory category){
			return skillSet[category];
		}
	}
}

