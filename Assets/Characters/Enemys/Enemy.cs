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
using SubBattleAbility = Parameter.CharacterParameters.SubBattleAbility;
using Faction = Parameter.CharacterParameters.Faction;
using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using HealSkillAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;


namespace Character{
    /// <summary>
    /// 敵のキャラクターオブジェクト
    /// マスターデータによって管理されます
    /// </summary>
	public class Enemy : IBattleable{
		private readonly int
			/// <summary> このキャラクターのID </summary>
			ID,
			/// <summary> このキャラクターの最大HP </summary>
			MAX_HP,
			/// <summary> このキャラクターの最大MP </summary>
			MAX_MP,
			/// <summary> このキャラクターの防御 </summary>
			DEF,
			/// <summary> このキャラクターのレベル </summary>
			LV,
			/// <summary> このキャラクターの通常ドロップのアイテムID </summary>
			NORMAL_DROP_ID,
			/// <summary> このキャラクターのレアドロップのアイテムID </summary>
			RARE_DROP_ID;

		private readonly string 
			/// <summary> このキャラクターの名前 </summary>
			NAME,
			/// <summary> このキャラクターのprefabのリソースのResourcesフォルダからの相対パス </summary>
			MODEL_ID;

		/// <summary> このキャラクターの能力値のDictonary </summary>
		private Dictionary<BattleAbility,int> abilities = new Dictionary<BattleAbility, int>();

		/// <summary> このキャラクターの現在HP </summary>
		private int hp;
		/// <summary> このキャラクターの現在MP </summary>
		private int mp;

		/// <summary> このキャラクターがバトルしているかどうか </summary>
		private bool isBattling = false;

		/// <summary> （実装予定）このキャラクターがカウンターするかどうか </summary>
		private bool isReadyToCounter = false;

		/// <summary> このキャラクターのcontainerオブジェクト </summary>
		private Container container;

		/// <summary> このキャラクターのAI </summary>
		private IEnemyAI ai;

		/// <summary> このキャラクターの個を表すID </summary>
		private readonly long UNIQE_ID;

		/// <summary> このキャラクターの陣営 </summary>
		private readonly Faction FACTION;
		
        /// <summary> このキャラクターが使うActiveSkillSet </summary>
		private ActiveSkillSet activeSkillSet;

		/// <summary> このキャラクターが使うReactionSkillSet </summary>
		private ReactionSkillSet reactionSkillSet;

		/// <summary> このキャラクターの能力値ボーナスを管理を管理するBonusKeeper </summary>
		private BonusKeeper bonusKeeper = new BonusKeeper();

        private Weapon equipedWeapon;

        private Dictionary<AttackSkillAttribute, float> attributeResistances = new Dictionary<AttackSkillAttribute, float>();

        private float deleteCount = 60f;

        private EnemyObserver observer;

        /// <summary>
        ///  <see cref="T:Character.Enemy"/> classのコンストラクタ
        /// </summary>
        /// <param name="builder">このキャラクターの初期設定を保持するEnemyBuilerクラス</param>
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

            this.MAX_HP = builder.getMaxHp();

            this.MAX_MP = builder.getMaxMp();

            this.hp = MAX_HP;
			this.mp = MAX_MP;

			GameObject prefab = (GameObject)Resources.Load(MODEL_ID);
			GameObject gameobject = MonoBehaviour.Instantiate (prefab);
			this.container = gameobject.GetComponent<Container> ();
			container.setCharacter(this);

			this.UNIQE_ID = UniqueIdCreator.creatUniqueId ();

            activeSkillSet = ActiveSkillSetMasterManager.getInstance().getActiveSkillSetFromId (builder.getActiveSkillSetId(),this);
            reactionSkillSet = ReactionSkillSetMasterManager.getInstance().getReactionSkillSetFromId (builder.getReactionSkillSetId());
			this.ai = EnemyAISummarizingManager.getInstance ().getAiFromId (builder.getAiId(),this,activeSkillSet,reactionSkillSet);

            attributeResistances = builder.getAttributeRegists();
            observer = new EnemyObserver(ID);
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

        public int getRawAbility(BattleAbility ability){
            return abilities[ability];
        }

        public int getAbilityContainsBonus(BattleAbility ability){
            return abilities[ability] + bonusKeeper.getBonus(ability);
        }

        public int getAtk (AttackSkillAttribute attribute, BattleAbility useAbility,bool useWepon) {
            int atk = getAbilityContainsBonus(useAbility) + UnityEngine.Random.Range(0,LV) + bonusKeeper.getBonus(SubBattleAbility.ATK);
            if (useWepon)
                atk += (equipedWeapon != null) ? equipedWeapon.attackWith() : 0;
            return atk;
		}

		public int getDef () {
            return DEF + bonusKeeper.getBonus(SubBattleAbility.DEF);
		}

		public void dammage (int dammage, AttackSkillAttribute attribute) {
			if (dammage < 0)
				dammage = 0;
            dammage = (int)((float)dammage * attributeResistances[attribute]);

            observer.dammaged(attribute);
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

		public int getDodge() {
			return getAbilityContainsBonus(BattleAbility.AGI);
		}

        public int getHit (BattleAbility useAbility,bool useWeapon) {
            int hitBonus = (useWeapon) ? (equipedWeapon != null) ? equipedWeapon.getHit() : 0 : 0;

            return getAbilityContainsBonus(useAbility) + UnityEngine.Random.Range (1,11) + hitBonus;
		}

		public int getHeal (BattleAbility useAbility) {
			throw new NotImplementedException ();
		}

		public float getCharacterDelay() {
            float delay;
            if(equipedWeapon != null){
                delay = equipedWeapon.getDelay();
            }else {
                float delayBonus = (float)abilities[BattleAbility.AGI] / 20;
                delayBonus = (delayBonus < 1.0f) ? delayBonus : 1.0f;
                delay = 2.0f - delayBonus;
            }
            return delay;
		}

		public int getCharacterRange() {
            return (equipedWeapon != null) ? equipedWeapon.getRange() : 0;
		}

		public BattleAbility getCharacterAttackMethod() {
            return (equipedWeapon != null) ? equipedWeapon.getWeaponAbility() : BattleAbility.MFT;
		}

		public void addAbilityBonus(SubBattleAbilityBonus bonus) {
            bonusKeeper.setBonus(bonus);
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
            if(!isBattling)
    			BattleManager.getInstance().joinBattle(this,FieldPosition.ONE,ai);
		}

        public Weapon getWeapon () {
			return this.equipedWeapon;
		}

		public void addAbilityBonus (BattleAbilityBonus bonus) {
			bonusKeeper.setBonus (bonus);
		}


		public void addSubAbilityBonus (SubBattleAbilityBonus bonus) {
			bonusKeeper.setBonus (bonus);
		}


		#endregion

		#region IChracter implementation

		public void act () {
            bonusKeeper.advanceLimit();
            deleteCount -= Time.deltaTime;
            if(deleteCount <= 0 && !isBattling){
                MonoBehaviour.Destroy(this.container.gameObject);
            }
		}

		public void death () {
            observer.killed();
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

		/// <summary>
        /// このキャラクターの種族を表すIDを取得します
        /// </summary>
        /// <returns>種族ID</returns>
		public int getId(){
			return this.ID;
		}

		/// <summary>
        /// このキャラクターが与える経験値を取得します
        /// </summary>
        /// <returns>与える経験値</returns>
		public int getGiveExp(){
			throw new NotSupportedException ();
		}

		/// <summary>
        /// このキャラクターのドロップアイテムを取得します
        /// なければnullを返します
        /// </summary>
        /// <returns>ドロップアイテム</returns>
		public IItem getDrop(){
			throw new NotSupportedException ();
		}

        public void progressAboutAttack(int madeDammage){
            observer.attacked(madeDammage);
        }

		public override string ToString () {
			return "Enemy " + this.NAME + " No. " + this.UNIQE_ID;
		}

		public override bool Equals (object obj) {
			//Enemyであり、IDが同値ならば等価と判断します
			if (!(obj is Enemy))
				return false;
			Enemy target = (Enemy)obj;
			if (target.getId() != this.getId())
				return false;
			return true;
		}
    }
}