
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using item;
using skill;
using parameter;
using battleSystem;

using System;

namespace character{
	public class Hero :Playable {

		public Hero(){
		}

		#region Character implementation
		public GameObject getModel () {
			throw new NotImplementedException ();
		}
		public void act () {
			throw new NotImplementedException ();
		}
		public void death () {
			throw new NotImplementedException ();
		}
		#endregion
		#region implemented abstract members of BattleableBase
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
		#region implemented abstract members of Playable
		public override void equipWepon (Wepon wepon) {
			throw new System.NotImplementedException ();
		}
		public override void equipArmor (Armor armor) {
			throw new System.NotImplementedException ();
		}
		public override void levelUp () {
			throw new System.NotImplementedException ();
		}
		#endregion
	}
}