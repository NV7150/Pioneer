using System.Collections.Generic;
using System;
using UnityEngine;

using Item;
using Parameter;
using AI;
using MasterData;
using BattleSystem;
using Skill;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using Faction = Parameter.CharacterParameters.Faction;
using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using HealSkillAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;


namespace Character{
	public class Enemy : IBattleable{
		private readonly int
			//このキャラクターのIDです
			ID,
			//このキャラクターの最大HPを表します
			MAX_HP,
			//このキャラクターの最大MPを表します
			MAX_MP,
			//このキャラクターの防御値です
			DEF,
			//このキャラクターのレベルです
			LV,
			//このキャラクターのノーマルドロップのアイテムIDです
			NORMAL_DROP_ID,
			//このキャラクターのレアドロップのアイテムIDです
			RARE_DROP_ID;

		private readonly string 
			//このキャラクターの名前です
			NAME,
			//このキャラクターのプレファブリソースへのパスです Resourcesフォルダ内からの相対パスになります
			MODEL_ID;

		//このキャラクターの各能力値を表します
		private Dictionary<CharacterParameters.BattleAbility,int> abilities = new Dictionary<CharacterParameters.BattleAbility, int>();

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

		//このキャラクターが得ている補正値のリストです
		private BonusKeeper bonusKeeper = new BonusKeeper();

		public Enemy(EnemyBuilder builder){
			this.ID = builder.getId ();
			this.NAME = builder.getName ();
			this.abilities = builder.getAbilities ();
			this.DEF = builder.getDef ();
			this.LV = builder.getLevel ();
			this.NORMAL_DROP_ID = builder.getNormalDropId ();
			this.RARE_DROP_ID = builder.getRareDropId ();
			this.MODEL_ID = builder.getModelName ();
			this.FACTION = builder.getFaction ();
			this.equipedWepon = builder.getWepon ();

//			this.MAX_HP = this.abilities [BattleAbility.PHY] * 2 + LV;
			this.MAX_HP = 100;

			this.MAX_MP = this.abilities [BattleAbility.MGP] * 2 + LV;

//			this.hp = MAX_HP;
			this.hp = 100;
			this.mp = MAX_MP;

			GameObject prefab = (GameObject)Resources.Load(MODEL_ID);
			GameObject gameobject = MonoBehaviour.Instantiate (prefab);
			this.container = gameobject.GetComponent<Container> ();
			container.setCharacter(this);

			this.UNIQE_ID = UniqueIdCreator.creatUniqueId ();

			activeSkillSet = ActiveSkillSetMasterManager.getActiveSkillSetFromId (builder.getActiveSkillSetId(),this);
			reactionSkillSet = ReactionSkillSetMasterManager.getReactionSkillSetFromId (builder.getReactionSkillSetId());
			this.ai = EnemyAISummarizingManager.getInstance ().getAiFromId (builder.getAiId(),this,activeSkillSet,reactionSkillSet);
		}
	    
			
		#region IBattleable implementation
		public int getHp () {
			return hp;
		}

		public int getMp () {
			return mp;
		}

		public int getMaxHp () {
			return MAX_HP;
		}

		public int getMaxMp () {
			return MAX_MP;
		}

		public int getMft () {
			return abilities[BattleAbility.MFT];
		}

		public int getFft () {
			return abilities[BattleAbility.FFT];
		}

		public int getMgp () {
			return abilities[BattleAbility.MGP];
		}

		public int getAgi () {
			return abilities[BattleAbility.AGI];
		}

		public int getPhy () {
			return abilities[BattleAbility.PHY];
		}

		public int getAtk (AttackSkillAttribute attribute, BattleAbility useAbility) {
			//もっとくふうすする予定
			return abilities [useAbility] + UnityEngine.Random.Range(0,10 + LV);
		}

		public int getDef () {
			return DEF;
		}

		public void dammage (int dammage, AttackSkillAttribute attribute) {
			if (dammage < 0)
				dammage = 0;
			//あとでじゃい
			this.hp -= dammage;
			if (this.hp < 0)
				this.hp = 0;
		}

		public void minusMp (int value) {
			if (value < 0)
				value = 0;

			this.mp -= value;
			if (this.mp < 0)
				this.mp = 0;
		}
			
		public void healed (int heal, HealSkillAttribute attribute) {
			Debug.Log ("" + heal);
			if (heal < 0 || attribute == HealSkillAttribute.NONE)
				throw new ArgumentException ("invlit heal");

			if (attribute == HealSkillAttribute.HP_HEAL || attribute == HealSkillAttribute.BOTH) {
				if (this.hp != 0)
					this.hp += heal;
			}
			if (attribute == HealSkillAttribute.MP_HEAL || attribute == HealSkillAttribute.BOTH) {
				this.mp += heal;
			}
			if (attribute == HealSkillAttribute.RESURRECTITION) {
				if(this.hp == 0)
					this.hp += heal;
			}
		}

		public int getHit (BattleAbility useAbility) {
			return abilities [useAbility] + UnityEngine.Random.Range (1,11);
		}

		public int healing (BattleAbility useAbility) {
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
			return LV;
		}
		List<IBattleable> decideTarget (List<IBattleable> bals) {
			throw new System.NotImplementedException ();
		}

		public Faction getFaction () {
			return FACTION;
		}

		public bool isHostility (Faction faction) {
			return (faction != FACTION);
		}

		public void encount(){
			BattleManager.getInstance().joinBattle(this,FieldPosition.ONE,ai);
		}

		public Wepon getWepon () {
			return this.equipedWepon;
		}

		public void addAbilityBonus (BattleAbilityBonus bonus) {
			bonusKeeper.setBonus (bonus);
		}


		public void addSubAbilityBonus (SubBattleAbilityBonus bonus) {
			bonusKeeper.setBonus (bonus);
		}

		#endregion

		#region IChracter implementation
		public GameObject getModel () {
			throw new NotImplementedException ();
		}

		public void act () {
			ENTP.hp = this.hp;
			ENTP.mp = this.mp;
		}

		public void death () {
			MonoBehaviour.Destroy (container);
		}

		public Container getContainer () {
			return this.container;
		}

		public string getName () {
			return this.NAME;
		}

		public long getUniqueId () {
			return this.UNIQE_ID;
		}
		#endregion

		//Enemyの種族IDを返します
		public int getId(){
			return this.ID;
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
			return "Enemy " + this.NAME + " No. " + this.UNIQE_ID;
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