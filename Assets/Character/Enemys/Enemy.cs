using System.Collections.Generic;
using System;
using UnityEngine;

using item;
using parameter;
using AI;
using masterData;

namespace character{
	public class Enemy : IBattleable{
		private int
			id,
			aiId,
			maxHp,
			maxMp,
			mft,
			fft,
			phy,
			mgp,
			agi,
			def,
			level,
			normalDropId,
			rareDropId,
			skillSetId;

		private string 
			name,
			modelName;

		private int hp;
		private int mp;
		private int defBonus = 0;
		private int dodgeBonus = 0;
		private int atkBonus = 0;

		private bool isBattling = false;
		private bool isReadyToCounter = false;

		private Container container;

		private IEnemyAI ai;

		public Enemy(EnemyBuilder builder){
			this.id = builder.getId ();
			this.name = builder.getName ();
			this.maxHp = builder.getMaxHp ();
			this.maxMp = builder.getMaxMp ();
			this.mft = builder.getMft ();
			this.fft = builder.getFft ();
			this.phy = builder.getPhy ();
			this.mgp = builder.getMgp ();
			this.agi = builder.getAgi ();
			this.def = builder.getDef ();
			this.level = builder.getLevel ();
			this.normalDropId = builder.getNormalDropId ();
			this.rareDropId = builder.getRareDropId ();
			this.skillSetId = builder.getSkillSetId ();
			this.modelName = builder.getModelName ();

			GameObject prefab = (GameObject)Resources.Load(modelName);
			GameObject gameobject = MonoBehaviour.Instantiate (prefab);
			this.container = gameobject.GetComponent<Container> ();
			container.setCharacter(this);
		}

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
			
		#region IBattleable implementation
		public int getHp () {
			return hp;
		}
		public void dammage (int dammage, skill.SkillType type) {
			throw new System.NotImplementedException ();
		}
		public int getMp () {
			return mp;
		}
		public void setHp (int hp) {
			if (hp < 0)
				throw new ArgumentException ("invlid hp");
			this.hp = hp;
		}
		public void setMp (int mp) {
			if (mp < 0)
				throw new ArgumentException ("invlid mp");
			this.mp = mp;
		}
		public int getMaxHp () {
			return maxHp;
		}
		public int getMaxMp () {
			return maxMp;
		}
		public int getMft () {
			return mft;
		}
		public int getFft () {
			return fft;
		}
		public int getMgp () {
			return mgp;
		}
		public int getAgi () {
			return agi;
		}
		public int getPhy () {
			return phy;
		}
		public int getDef () {
			return def;
		}
		public float getDelay (skill.IActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public bool getIsBattling () {
			throw new System.NotImplementedException ();
		}
		public void setIsBattling (bool boolean) {
			isBattling = boolean;
		}
		public int move () {
			throw new System.NotImplementedException ();
		}
		public void syncronizePositioin (UnityEngine.Vector3 vector) {
			container.getModel ().transform.position = vector;
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
			defBonus = bonus;
		}
		public void setDodBonus (int bonus) {
			dodgeBonus = bonus;
		}
		public void setAtkBonus (int bonus) {
			atkBonus = bonus;
		}
		public void setIsReadyToCounter (bool flag) {
			isReadyToCounter = flag;
		}
		public void resetBonus () {
			defBonus = 0;
			dodgeBonus = 0;
			atkBonus = 0;
			isReadyToCounter = false;
		}
		public int getLevel () {
			return level;
		}
		List<IBattleable> IBattleable.decideTarget (List<IBattleable> bals) {
			throw new System.NotImplementedException ();
		}

		#endregion

		#region IChracter implementation
		public GameObject getModel () {
			throw new NotImplementedException ();
		}

		public void act () {
//			Debug.Log ("Succesed");
		}

		public void death () {
			throw new NotImplementedException ();
		}
		#endregion

		public int getId(){
			return this.id;
		}
	}
}