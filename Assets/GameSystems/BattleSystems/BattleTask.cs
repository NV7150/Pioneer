using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using skill;
using character;

namespace battleSystem{
	public class BattleTask {
		private readonly string name;
		private readonly ActiveSkill SKILL;
		private readonly List<IBattleable> TARGETS;
		private readonly long OWNER_UNIQUEID;

		public BattleTask(long uniqueId,ActiveSkill skill,List<IBattleable> targets){
			this.OWNER_UNIQUEID = uniqueId;
			this.name = skill.getName ();
			this.SKILL = skill;
			this.TARGETS = targets;
		}

		public ActiveSkill getSkill(){
			return SKILL;
		}

		public List<IBattleable> getTargets(){
			return TARGETS;
		}

		public long getOwnerBattleId(){
			return OWNER_UNIQUEID;
		}
	}
}
