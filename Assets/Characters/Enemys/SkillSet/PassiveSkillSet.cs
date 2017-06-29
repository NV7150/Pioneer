using System;
using System.Collections;
using System.Collections.Generic;

using Skill;
using AI;
using MasterData;

namespace AI {
	public class PassiveSkillSet {

		private readonly int ID;
		private readonly string NAME;
		private Dictionary<PassiveSkillCategory,PassiveSkill> skillSet;

		public PassiveSkillSet (PassiveSkillSetBuilder builder) {
			this.ID = builder.getId ();
			this.NAME = builder.getName ();
			this.skillSet = builder.getSet ();
		}

		public int getId(){
			return ID;
		}

		public string getName(){
			return NAME;
		}

		public PassiveSkill getSkillFromCategory(PassiveSkillCategory category){
			return skillSet [category];
		}
	}
}

