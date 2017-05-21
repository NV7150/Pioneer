
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using item;
using skill;
using parameter;
using battleSystem;

using System;

namespace character{
	public class Hero :PlayableBase {

		public Hero(Dictionary<Ability,int> parameters) : base(parameters){
		}

		#region Character implementation
		public override GameObject getModel () {
			throw new NotImplementedException ();
		}
		public override void act () {
			throw new NotImplementedException ();
		}
		public override void death () {
			throw new NotImplementedException ();
		}
		#endregion
		#region implemented abstract members of BattleableBase
		public override int getDef () {
			throw new System.NotImplementedException ();
		}
		public override float getDelay (IActiveSkill skill) {
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
		public override IActiveSkill decideSkill () {
			throw new System.NotImplementedException ();
		}
		public override int getRange (IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public override List<BattleableBase> decideTarget (List<BattleableBase> bals) {
			throw new System.NotImplementedException ();
		}
		public override int getHitness (IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public override int battleAction (IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public override IPassiveSkill decidePassiveSkill () {
			throw new System.NotImplementedException ();
		}
		public override int getDodgeNess () {
			throw new System.NotImplementedException ();
		}
		#endregion
		#region implemented abstract members of Playable
		public override void equipWepon (WeponBase wepon) {
			throw new System.NotImplementedException ();
		}
		public override void equipArmor (ArmorBase armor) {
			throw new System.NotImplementedException ();
		}
		public override void levelUp () {
			throw new System.NotImplementedException ();
		}
		#endregion
	}
}