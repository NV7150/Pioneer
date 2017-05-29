using System.Collections.Generic;
using System;

using item;
using parameter;

namespace character{
	public class Enemy : IBattleable{
	    //エンカウントし、戦闘に突入します
		public void encount(){
			throw new NotSupportedException ();
		}

	    //このEnemyが与える経験値を取得します
		public int getGiveExp(){
			throw new NotSupportedException ();
		}

	    //このEnemyのドロップアイテムを取得します。ない場合はnullを返します
		public IItem getDrop(){
			throw new NotSupportedException ();
		}

		public int getMaxHp () {
			throw new System.NotImplementedException ();
		}
		public int getMaxMp () {
			throw new System.NotImplementedException ();
		}



		#region IBattleable implementation
		public int getHp () {
			throw new System.NotImplementedException ();
		}
		public void dammage (int dammage, skill.SkillType type) {
			throw new System.NotImplementedException ();
		}
		public int getMp () {
			throw new System.NotImplementedException ();
		}
		public void setHp (int hp) {
			throw new System.NotImplementedException ();
		}
		public void setMp (int mp) {
			throw new System.NotImplementedException ();
		}
		public int getMft () {
			throw new System.NotImplementedException ();
		}
		public int getFft () {
			throw new System.NotImplementedException ();
		}
		public int getMgp () {
			throw new System.NotImplementedException ();
		}
		public int getAgi () {
			throw new System.NotImplementedException ();
		}
		public int getPhy () {
			throw new System.NotImplementedException ();
		}
		public int getDef () {
			throw new System.NotImplementedException ();
		}
		public float getDelay (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public bool getIsBattling () {
			throw new System.NotImplementedException ();
		}
		public void setIsBattling (bool boolean) {
			throw new System.NotImplementedException ();
		}
		public int move () {
			throw new System.NotImplementedException ();
		}
		public void syncronizePositioin (UnityEngine.Vector3 vector) {
			throw new System.NotImplementedException ();
		}
		public battleSystem.BattleCommand decideCommand () {
			throw new System.NotImplementedException ();
		}
		public skill.IActiveSkill decideSkill () {
			throw new System.NotImplementedException ();
		}
		public int getRange (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public int getHitness (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public int battleAction (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public skill.IPassiveSkill decidePassiveSkill () {
			throw new System.NotImplementedException ();
		}
		public int getDodgeness () {
			throw new System.NotImplementedException ();
		}
		public void setDefBonus (int bonus) {
			throw new System.NotImplementedException ();
		}
		public void setDodBonus (int bonus) {
			throw new System.NotImplementedException ();
		}
		public void setAtkBonus (int bonus) {
			throw new System.NotImplementedException ();
		}
		public void setDoCounter (bool flag) {
			throw new System.NotImplementedException ();
		}
		public void resetBonus () {
			throw new System.NotImplementedException ();
		}
		public int getLevel () {
			throw new System.NotImplementedException ();
		}
		List<IBattleable> IBattleable.decideTarget (List<IBattleable> bals) {
			throw new System.NotImplementedException ();
		}

		#endregion
		#region ICharacter implementation
		public UnityEngine.GameObject getModel () {
			throw new System.NotImplementedException ();
		}
		public void act () {
			throw new System.NotImplementedException ();
		}
		public void death () {
			throw new System.NotImplementedException ();
		}
		#endregion
	}
}
