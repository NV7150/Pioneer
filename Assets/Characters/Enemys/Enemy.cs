using System.Collections.Generic;
using System;
using UnityEngine;

using Item;
using Parameter;
using AI;
using MasterData;
using BattleSystem;
using Skill;

namespace Character{
	public class Enemy : IBattleable{
		private int
			id,
			def,
			level,
			normalDropId,
			rareDropId;

		private string 
			name,
			modelName;

		private Dictionary<Ability,int> abilities = new Dictionary<Ability, int>();

		private int hp;
		private int mp;
		private int defBonus = 0;
		private int dodgeBonus = 0;
		private int atkBonus = 0;

		private bool isBattling = false;

		private bool isReadyToCounter = false;

		private Container container;

		private IEnemyAI ai;

		private readonly long UNIQE_ID;

		private readonly Faction FACTION;

		private ActiveSkillSet activeSkillSet;
		private PassiveSkillSet passiveSkillSet;

		public Enemy(EnemyBuilder builder){
			this.id = builder.getId ();
			this.name = builder.getName ();
			this.abilities = builder.getAbilities ();
			this.def = builder.getDef ();
			this.level = builder.getLevel ();
			this.normalDropId = builder.getNormalDropId ();
			this.rareDropId = builder.getRareDropId ();
			this.modelName = builder.getModelName ();
			this.FACTION = builder.getFaction ();

			GameObject prefab = (GameObject)Resources.Load(modelName);
			GameObject gameobject = MonoBehaviour.Instantiate (prefab);
			this.container = gameobject.GetComponent<Container> ();
			container.setCharacter(this);

			this.UNIQE_ID = UniqueIdCreator.creatUniqueId ();

			activeSkillSet = ActiveSkillSetMasterManager.getActiveSkillSetFromId (builder.getActiveSkillSetId());
			passiveSkillSet = PassiveSkillSetMasterManager.getPassiveSkillSetFromId (builder.getPassiveSkillSetId());
			this.ai = EnemyAISummarizingManager.getInstance ().getAiFromId (builder.getAiId(),this,activeSkillSet,passiveSkillSet);

			this.hp = abilities [Ability.HP];
		}

	    //エンカウントし、戦闘に突入します
		public void encount(){
			container.getExcecutor().StartCoroutine(BattleManager.getInstance().joinBattle(this,FieldPosition.ONE,ai));
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
			return abilities[Ability.HP];
		}
		public int getMaxMp () {
			return abilities[Ability.MP];
		}
		public int getMft () {
			return abilities[Ability.MFT];
		}
		public int getFft () {
			return abilities[Ability.FFT];
		}
		public int getMgp () {
			return abilities[Ability.MGP];
		}
		public int getAgi () {
			return abilities[Ability.AGI];
		}
		public int getPhy () {
			return abilities[Ability.PHY];
		}

		public int getAtk (SkillAttribute attribute, Ability useAbility) {
			//もっとくふうすする予定
			return abilities [useAbility];
		}

		public int getDef () {
			return def;
		}
		public float getDelay () {
			throw new System.NotImplementedException ();
		}
		public bool getIsBattling () {
			return isBattling;
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
		public BattleCommand decideCommand () {
			throw new System.NotImplementedException ();
		}
		public ActiveSkill decideSkill () {
			throw new System.NotImplementedException ();
		}
		public int getRange (ActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public int getHitness (ActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public int battleAction (ActiveSkill skill) {
			throw new System.NotImplementedException ();
		}
		public PassiveSkill decidePassiveSkill () {
			throw new System.NotImplementedException ();
		}
		public int getDodgeness () {
			//あとで実装
			return getAgi ();
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

		public Faction getFaction () {
			return FACTION;
		}

		public bool isHostility (Faction faction) {
			return (faction == FACTION);
		
		}

		public string getName () {
			return this.name;
		}

		public long getUniqueId () {
			return this.UNIQE_ID;
		}
		#endregion

		#region IChracter implementation
		public GameObject getModel () {
			throw new NotImplementedException ();
		}

		public void act () {
//			Debug.Log ("" + this.hp);
		}

		public void death () {
			throw new NotImplementedException ();
		}

		public Container getContainer () {
			return this.container;
		}
		#endregion

		public int getId(){
			return this.id;
		}

		public override bool Equals (object obj) {
			//Enemyであり、IDとユニークIDが同値ならば等価と判断します
			if (!(obj is Enemy))
				return false;
			Enemy target = (Enemy)obj;
			if (target.getUniqueId () != this.getUniqueId() && target.getId() != this.getId())
				return false;
			return true;
		}

		public void dammage (int dammage, SkillAttribute attribute) {
			if (dammage < 0)
				dammage = 0;
			//あとでじゃい
			this.hp -= dammage;
			if (this.hp < 0)
				this.hp = 0;
		}

		public void healed (int heal, HealAttribute attribute) {
			throw new NotImplementedException ();
		}

		public float getDelay (float delay) {
			throw new NotImplementedException ();
		}

		public int getRange (int range) {
			throw new NotImplementedException ();
		}

		public int getHitness (Ability useAbility) {
			return abilities [useAbility] + UnityEngine.Random.Range (1,11);
		}

		public int attack (int baseParameter, Ability useAbility) {
			throw new NotImplementedException ();
		}

		public int healing (int baseParameter, Ability useAbility) {
			throw new NotImplementedException ();
		}

		public override string ToString () {
			return "Enemy " + this.name + " No. " + this.UNIQE_ID;
		}
	}
}