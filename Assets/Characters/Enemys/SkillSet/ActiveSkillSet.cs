using System;
using System.Collections;
using System.Collections.Generic;

using Skill;
using AI;
using Parameter;
using MasterData;

namespace AI {
	public class ActiveSkillSet {
		private Dictionary<ActiveSkillCategory,ActiveSkill> skillSet = new Dictionary<ActiveSkillCategory, ActiveSkill> ();
		private int id;
		private int	maxRange;
		private string name;

		public ActiveSkillSet (ActiveSkillSetBuilder builder) {
			this.id = builder.getId ();
			this.name = builder.getName ();

			skillSet [ActiveSkillCategory.NORMAL] = builder.getNormalSkill ();
			skillSet [ActiveSkillCategory.CAUTION] = builder.getCautionSkill ();
			skillSet [ActiveSkillCategory.DANGER] = builder.getDangerSkill ();
			skillSet [ActiveSkillCategory.POWER] = builder.getPowerSkill ();
			skillSet [ActiveSkillCategory.FULL_POWER] = builder.getFullPowerSkill ();
			skillSet [ActiveSkillCategory.SUPPORT] = builder.getSupportSkill ();
			skillSet [ActiveSkillCategory.HEAL] = builder.getHealSkill ();
			skillSet [ActiveSkillCategory.MOVE] = builder.getMoveSkill ();

			calculateMaxRange ();
		}

		private int calculateMaxRange(){
			int maxRange = 0;
			foreach(ActiveSkillCategory category in skillSet.Keys){
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

		public ActiveSkill getSkillFromSkillCategory(ActiveSkillCategory category){
			return skillSet[category];
		}
	}
}

