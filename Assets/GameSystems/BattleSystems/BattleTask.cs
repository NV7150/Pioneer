using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Skill;
using Character;

namespace BattleSystem{
	public class BattleTask {
		private readonly string NAME;
		private readonly ActiveSkill SKILL;
		private List<IBattleable> targets;
		private int move;
		private readonly long OWNER_UNIQUEID;


		public BattleTask(long uniqueId,ActiveSkill skill,List<IBattleable> targets){
			this.OWNER_UNIQUEID = uniqueId;
			this.NAME = skill.getName ();
			this.SKILL = skill;
			this.targets = targets;
		}

		public BattleTask(long uniqueId,ActiveSkill skill,int move){
			this.OWNER_UNIQUEID = uniqueId;
			this.NAME = skill.getName ();
			this.SKILL = skill;
			this.move = move;
		}

		public ActiveSkill getSkill(){
			return SKILL;
		}

		public List<IBattleable> getTargets(){
			if (targets.Count == 0)
				throw new InvalidOperationException ("this task isn't an action");
			return targets;
		}

		public int getMove(){
			return move;
		}

		public long getOwnerBattleId(){
			return OWNER_UNIQUEID;
		}
	}
}
