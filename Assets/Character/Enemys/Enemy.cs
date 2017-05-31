using System.Collections.Generic;
using System;
using UnityEngine;

using item;
using parameter;
using AI;

namespace character{
	[System.SerializableAttribute]
	public class Enemy : IBattleable{
		[SerializeField]
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

		[SerializeField]
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

		public Enemy(string[] data){
			setParameterFromCSV (data);
//			container = new Container ((GameObject)Resources.Load(modelName),this);
			GameObject gameobject = (GameObject)Resources.Load(modelName);
//			this.container = gameobject.GetComponent<Container> ();
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
		#region ICharacter implementation
		public UnityEngine.GameObject getModel () {
			return container.getModel ();
		}
		public void act () {
			Debug.Log ("Succesed");
		}
		public void death () {
			throw new System.NotImplementedException ();
		}
		#endregion

		private void setParameterFromCSV(string[] parameters){
			id = int.Parse (parameters [0]);
			name = parameters [1];
			aiId = int.Parse (parameters [2]);
			maxHp = int.Parse (parameters [3]);
			maxMp = int.Parse (parameters [4]);
			mft = int.Parse (parameters[5]);
			fft = int.Parse (parameters [6]);
			phy = int.Parse (parameters [7]);
			mgp = int.Parse (parameters [8]);
			agi = int.Parse (parameters [9]);
			def = int.Parse (parameters [10]);
			level = int.Parse (parameters [11]);
			normalDropId = int.Parse (parameters [12]);
			rareDropId = int.Parse (parameters [13]);
			skillSetId = int.Parse (parameters [14]);
			modelName = "Models/" + parameters [15];
		}

		public Enemy Clone(){
			Enemy en = new Enemy (
				new string[] {
					"" + id,
					name,
					"" + aiId,
					"" + maxHp,
					"" + maxMp,
					"" + maxMp,
					"" + mft,
					"" + fft,
					"" + phy,
					"" + mgp,
					"" + agi,
					"" + def,
					"" + level,
					"" + normalDropId,
					"" + rareDropId,
					"" + skillSetId,
					modelName.Remove(0,7)
				}
			);
			MonoBehaviour.Instantiate (en.getModel());
			return en;
		}

		public int getId(){
			return this.id;
		}
	}
}