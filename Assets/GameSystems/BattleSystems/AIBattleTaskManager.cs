using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;
using Skill;
using Character;

namespace BattleSystem {
	public class AIBattleTaskManager : MonoBehaviour,IBattleTaskManager{
		IEnemyAI ai;
		IBattleable bal;

		public void setCharacter(IBattleable bal,IEnemyAI ai){
			this.ai = ai;
			this.bal = bal;
		}

		private void searchIsReady(){
			if (ai == null || bal == null)
				throw new InvalidOperationException ("Manager hasn't readied yet");
		}

		#region IBattleTaskManager implementation
		public BattleTask getTask () {
			searchIsReady ();
			ActiveSkill skill = ai.decideSkill ();
			List<IBattleable> targets = ai.decideTarget (BattleManager.getInstance().getCharacterInRange(bal,skill.getRange()),skill);
			return new BattleTask (bal.getUniqueId(),skill,targets);
		}

		public void deleteTaskFromTarget (Character.IBattleable target) {/*実装する必要ないです*/}

		public void offerPassive () {/*実装する必要ないです*/}

		public PassiveSkill getPassive (IBattleable attacker,ActiveSkill skill) {
			searchIsReady ();
			return ai.decidePassive (attacker,skill);
		}

		public bool isHavingTask () {
			return true;
		}
		#endregion
	}
}

