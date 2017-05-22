
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using item;
using skill;
using parameter;
using battleSystem;

using System;

namespace character{
	public class Hero :IPlayable {
		#region IPlayable implementation
		public void equipWepon (WeponBase wepon) {
			throw new NotImplementedException ();
		}
		public void equipArmor (ArmorBase armor) {
			throw new NotImplementedException ();
		}
		public void levelUp () {
			throw new NotImplementedException ();
		}
		public void addExp (int val) {
			throw new NotImplementedException ();
		}
		public int getExp () {
			throw new NotImplementedException ();
		}
		#endregion
		#region IFriendly implementation
		public int getSpc () {
			throw new NotImplementedException ();
		}
		public void talk (IFriendly friendly) {
			throw new NotImplementedException ();
		}
		#endregion
		#region IBattleable implementation
		public int getHp () {
			throw new NotImplementedException ();
		}
		public void dammage (int dammage, SkillType type) {
			throw new NotImplementedException ();
		}
		public int getMp () {
			throw new NotImplementedException ();
		}
		public int setHp (int hp) {
			throw new NotImplementedException ();
		}
		public int setMp (int mp) {
			throw new NotImplementedException ();
		}
		public int getMft () {
			throw new NotImplementedException ();
		}
		public int getFft () {
			throw new NotImplementedException ();
		}
		public int getMgp () {
			throw new NotImplementedException ();
		}
		public int getAgi () {
			throw new NotImplementedException ();
		}
		public int getPhy () {
			throw new NotImplementedException ();
		}
		public int getDef () {
			throw new NotImplementedException ();
		}
		public float getDelay (IActiveSkill skill) {
			throw new NotImplementedException ();
		}
		public bool getIsBattling () {
			throw new NotImplementedException ();
		}
		public void setIsBattling (bool boolean) {
			throw new NotImplementedException ();
		}
		public int move () {
			throw new NotImplementedException ();
		}
		public void syncronizePositioin (Vector3 vector) {
			throw new NotImplementedException ();
		}
		public BattleCommand decideCommand () {
			throw new NotImplementedException ();
		}
		public IActiveSkill decideSkill () {
			throw new NotImplementedException ();
		}
		public int getRange (IActiveSkill skill) {
			throw new NotImplementedException ();
		}
		public List<IBattleable> decideTarget (List<IBattleable> bals) {
			throw new NotImplementedException ();
		}
		public int getHitness (IActiveSkill skill) {
			throw new NotImplementedException ();
		}
		public int battleAction (IActiveSkill skill) {
			throw new NotImplementedException ();
		}
		public IPassiveSkill decidePassiveSkill () {
			throw new NotImplementedException ();
		}
		public int getDodgeness () {
			throw new NotImplementedException ();
		}
		public void setDefBonus (int bonus) {
			throw new NotImplementedException ();
		}
		public void setDodBonus (int bonus) {
			throw new NotImplementedException ();
		}
		public void setAtkBonus (int bonus) {
			throw new NotImplementedException ();
		}
		public void setDoCounter (bool flag) {
			throw new NotImplementedException ();
		}
		public void resetBonus () {
			throw new NotImplementedException ();
		}
		public int getLevel () {
			throw new NotImplementedException ();
		}
		#endregion
		#region ICharacter implementation
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
	}
}