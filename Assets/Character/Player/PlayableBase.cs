using character;
using item;
using parameter;

using System;
using System.Collections.Generic;

namespace character{
	public abstract class PlayableBase : IBattleable{
		//経験値を表します
		protected int exp = 0;
		//次のレベルアップに必要なexpを表します。
		protected int needExp;

		//対象(武器)を装備します
		public abstract void equipWepon(WeponBase wepon);

		//対象(防具)を装備します
		public abstract void equipArmor(ArmorBase armor);

		//レベルアップし、能力値を成長させます
		public abstract void levelUp();

		//経験値を取得します
		public void addExp(int val){
			if (!(0 < val))
				throw new ArgumentException ("You tried to add wrong value in addExp.");
			exp += val;
		}

		//経験値を返します
		public int getExp(){
			return exp;
		}

		#region IBattleable implementation
		public int getHp () {
			throw new NotImplementedException ();
		}
		public void dammage (int dammage, skill.SkillType type) {
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
		public float getDelay (skill.IActiveSkill skill) {
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
		public void syncronizePositioin (UnityEngine.Vector3 vector) {
			throw new NotImplementedException ();
		}
		public battleSystem.BattleCommand decideCommand () {
			throw new NotImplementedException ();
		}
		public skill.IActiveSkill decideSkill () {
			throw new NotImplementedException ();
		}
		public int getRange (skill.IActiveSkill skill) {
			throw new NotImplementedException ();
		}
		public List<IBattleable> decideTarget (List<IBattleable> bals) {
			throw new NotImplementedException ();
		}
		public int getHitness (skill.IActiveSkill skill) {
			throw new NotImplementedException ();
		}
		public int battleAction (skill.IActiveSkill skill) {
			throw new NotImplementedException ();
		}
		public skill.IPassiveSkill decidePassiveSkill () {
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
		public UnityEngine.GameObject getModel () {
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
