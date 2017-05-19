using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using character;
using battleSystem;
using skill;

namespace character{
	public class Goblin : Enemy {

		private int hp;
		private int mp;

		private readonly int LEVEL= 1;

		private readonly int MFT;
		private readonly int FFT;
		private readonly int AGI;
		private readonly int PHY;
		private readonly int MGP;
		private readonly int DEF;

		private readonly int EXP = 10;
		private readonly int MET = 5;

		private bool isButtling = false;
		private bool doCounter;

		private Container container;

		private ActiveSkill commandOne = new NormalAttack ();

		public Goblin(Container container){
			System.Random random = new System.Random (100);
			MFT = 3;
			FFT = 0;
			AGI = 0;
			PHY = 1 + random.Next (0, 2);
			MGP = 0;
			DEF = MFT / 2;

			this.container = container;
		}

		// Use this for initialization
		void Start () {hp = 100;}
		
		// Update is called once per frame
		void Update () {
//			Debug.Log ("enemy : " + hp);
		}

		#region Enemy implementation

		public void encount () {
			isButtling = true;
			container.getExcecutor().StartCoroutine(BattleManager.getInstance ().joinBattle (this,FiealdPosition.MTHREE));

		}

		public int getGiveExp () {
			return EXP;
		}

		public item.Item getDrop () {
			throw new System.NotImplementedException ();
		}

		#endregion

		#region NonPlayerCharacter implementation

		public void act () {
			//処理を記入
		}

		#endregion

		#region Battleable implementation

		public int getHp () {
			return hp;
		}

		public void dammage (int dammage, skill.SkillType type) {
			hp -= dammage;
		}

		public int getMp () {
			return mp;
		}

		public void divMp (int value) {
			mp -= value;
		}

		public void heal (int value) {
			hp += value;
		}

		public void healMp (int value) {
			mp += value;
		}

		public int getMft () {
			return MFT;
		}

		public int getFft () {
			return FFT;
		}

		public int getMgp () {
			return MGP;
		}

		public int getAgi () {
			return AGI;
		}

		public int getPhy () {
			return PHY;
		}

		public int getDef () {
			return DEF;
		}

		public float BattleableBase.getDelay (ActiveSkill skill) {
			return skill.getDelay (this,0.5f);
		}

		public battleSystem.AttackType getAttackType () {
			return AttackType.MELEE;
		}

		public bool getIsBattling () {
			return isButtling;
		}

		public void setIsBattling (bool boolean) {
			isButtling = true;
		}

		public int move () {
			return AGI / 5 + 1;
		}

		public BattleCommand decideCommand () {
			return BattleCommand.MOVE;
		}

		public skill.ActiveSkill decideSkill () {
			return commandOne;
		}

		public int getRange (skill.ActiveSkill skill) {
			return skill.getRange(this,0);
		}

		public int getBasicRange () {
			return 0;
		}

		public List<BattleableBase> target (List<BattleableBase> bals) {
			return bals;
		}

		public int getHitness (skill.ActiveSkill skill) {
			return skill.getSuccessRate (this);
		}

		public int buttleAction (skill.ActiveSkill skill) {
			return skill.use (this);
		}

		public skill.PassiveSkill decidePassive () {
			return new NoGuard ();
		}

		public int dodgeSuccessed () {
			return AGI;
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
			return LEVEL;
		}

		#endregion

		#region Character implementation

		public GameObject getModel () {
			return container.getModel ();
		}

		public void death () {
			throw new System.NotImplementedException ();
		}

		#endregion

		public void BattleableBase.syncronizePositioin (Vector3 vector) {
			container.getModel ().transform.position = vector;
		}

	}
}
