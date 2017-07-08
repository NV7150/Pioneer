using System.Collections.Generic;
using System;
using UnityEngine;

using Item;
using Parameter;
using AI;
using MasterData;
using BattleSystem;
using Skill;

using Ability = Parameter.CharacterParameters.Ability;
using Faction = Parameter.CharacterParameters.Faction;
using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using HealSkillAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;


namespace Character{
	public class Enemy : IBattleable{
		private int
			//このキャラクターのIDです
			id,
			//このキャラクターの防御値ですsd
			def,
			//このキャラクターのレベルです
			level,
			//このキャラクターのノーマルドロップのアイテムIDです
			normalDropId,
			//このキャラクターのレアドロップのアイテムIDです
			rareDropId;

		private string 
			//このキャラクターの名前です
			name,
			//このキャラクターのプレファブリソースへのパスです Resourcesフォルダ内からの相対パスになります
			modelName;

		//このキャラクターの各能力値を表します
		private Dictionary<CharacterParameters.Ability,int> abilities = new Dictionary<CharacterParameters.Ability, int>();


		//このキャラクターの現在のHPです
		private int hp;
		//このキャラクターの現在のMPです
		private int mp;

		//このキャラクターがバトル中かどうかを表します
		private bool isBattling = false;

		//このキャラクターがカウンターを行うかを表します
		private bool isReadyToCounter = false;

		//このキャラクターのゲームオブジェクトにアタッチされてるContainerオブジェクトを表します
		private Container container;

		//このキャラクターのAIを表します
		private IEnemyAI ai;

		//このキャラクターの個を識別するためのIDです
		private readonly long UNIQE_ID;

		//このキャラクターが属する陣営を表します
		private readonly Faction FACTION;
		//このキャラクターが持つActiveSkillSetです
		private ActiveSkillSet activeSkillSet;

		//このキャラクターが持つPassiveSkillSetです
		private ReactionSkillSet reactionSkillSet;

		//このキャラクターの武器です
		private Wepon equipedWepon;

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
			this.equipedWepon = builder.getWepon ();

			GameObject prefab = (GameObject)Resources.Load(modelName);
			GameObject gameobject = MonoBehaviour.Instantiate (prefab);
			this.container = gameobject.GetComponent<Container> ();
			container.setCharacter(this);

			this.UNIQE_ID = UniqueIdCreator.creatUniqueId ();

			activeSkillSet = ActiveSkillSetMasterManager.getActiveSkillSetFromId (builder.getActiveSkillSetId(),this);
			reactionSkillSet = ReactionSkillSetMasterManager.getReactionSkillSetFromId (builder.getReactionSkillSetId());
			this.ai = EnemyAISummarizingManager.getInstance ().getAiFromId (builder.getAiId(),this,activeSkillSet,reactionSkillSet);

			this.hp = abilities [Ability.HP];
		}
	    
			
		#region IBattleable implementation
		public int getHp () {
			return hp;
		}

		public int getMp () {
			return mp;
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

		public int getAtk (AttackSkillAttribute attribute, Ability useAbility) {
			//もっとくふうすする予定
			return abilities [useAbility];
		}

		public int getDef () {
			return def;
		}

		public int getDelay () {
			throw new System.NotImplementedException ();
		}

		public void dammage (int dammage, AttackSkillAttribute attribute) {
			if (dammage < 0)
				dammage = 0;
			//あとでじゃい
			this.hp -= dammage;
			if (this.hp < 0)
				this.hp = 0;
		}
			
		public void healed (int heal, HealSkillAttribute attribute) {
			throw new NotImplementedException ();
		}

		public int getHit (Ability useAbility) {
			return abilities [useAbility] + UnityEngine.Random.Range (1,11);
		}

		public int healing (int baseParameter, Ability useAbility) {
			throw new NotImplementedException ();
		}

		public bool getIsBattling () {
			return isBattling;
		}

		public void setIsBattling (bool boolean) {
			isBattling = boolean;
		}

		public void syncronizePositioin (UnityEngine.Vector3 vector) {
			container.getModel ().transform.position = vector;
		}

		public int getDodge () {
			//あとで実装
			return getAgi ();
		}
		public void setIsReadyToCounter (bool flag) {
			isReadyToCounter = flag;
		}

		public void resetBonus () {
			throw new NotImplementedException ();
		}

		public int getLevel () {
			return level;
		}
		List<IBattleable> decideTarget (List<IBattleable> bals) {
			throw new System.NotImplementedException ();
		}

		public Faction getFaction () {
			return FACTION;
		}

		public bool isHostility (Faction faction) {
			return (faction == FACTION);
		}

		//エンカウントし、戦闘に突入します
		public void encount(){
			BattleManager.getInstance().joinBattle(this,FieldPosition.ONE,ai);
		}

		public Wepon getWepon () {
			return this.equipedWepon;
		}

		public void addAbilityBonus (AbilityBonus bonus) {
			throw new NotImplementedException ();
		}


		public void addSubAbilityBonus (SubAbilityBonus bonus) {
			throw new NotImplementedException ();
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

		public string getName () {
			return this.name;
		}

		public long getUniqueId () {
			return this.UNIQE_ID;
		}
		#endregion

		//Enemyの種族IDを返します
		public int getId(){
			return this.id;
		}

		//HPを設定します
		public void setHp (int hp) {
			if (hp < 0)
				throw new ArgumentException ("invalid hp");
			this.hp = hp;
		}

		//MPを設定します
		public void setMp (int mp) {
			if (mp < 0)
				throw new ArgumentException ("invalid mp");
			this.mp = mp;
		}

		//このEnemyが与える経験値を取得します
		public int getGiveExp(){
			throw new NotSupportedException ();
		}

		//このEnemyのドロップアイテムを取得します。ない場合はnullを返します
		public IItem getDrop(){
			throw new NotSupportedException ();
		}

		public override string ToString () {
			return "Enemy " + this.name + " No. " + this.UNIQE_ID;
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
	}
}