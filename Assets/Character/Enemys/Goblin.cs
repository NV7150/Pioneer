using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using character;
using battleSystem;
using skill;

namespace character{
	public class Goblin : Enemy {
		#region implemented abstract members of BattleableBase

		public override GameObject getModel () {
			throw new System.NotImplementedException ();
		}

		public override void act () {
			throw new System.NotImplementedException ();
		}

		public override void death () {
			throw new System.NotImplementedException ();
		}

		public override int getDef () {
			throw new System.NotImplementedException ();
		}
		public override float getDelay (ActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public override int move () {
			throw new System.NotImplementedException ();
		}
		public override void syncronizePositioin (Vector3 vector) {
			throw new System.NotImplementedException ();
		}
		public override BattleCommand decideCommand () {
			throw new System.NotImplementedException ();
		}
		public override ActiveSkill decideSkill () {
			throw new System.NotImplementedException ();
		}
		public override int getRange (ActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public override List<BattleableBase> decideTarget (List<BattleableBase> bals) {
			throw new System.NotImplementedException ();
		}
		public override int getHitness (ActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public override int battleAction (ActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public override PassiveSkill decidePassiveSkill () {
			throw new System.NotImplementedException ();
		}
		public override int getDodgeNess () {
			throw new System.NotImplementedException ();
		}
		#endregion
		#region implemented abstract members of Enemy
		public override void encount () {
			throw new System.NotImplementedException ();
		}
		public override int getGiveExp () {
			throw new System.NotImplementedException ();
		}
		public override item.Item getDrop () {
			throw new System.NotImplementedException ();
		}
		#endregion

		public Goblin(BattleableBaseBuilder builder) : base(builder){
		}

	}
}
