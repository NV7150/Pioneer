﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Item;
using Skill;
using Parameter;
using BattleSystem;
using Menus;
using Quest;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;
using Faction = Parameter.CharacterParameters.Faction;
using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using HealSkillAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;
using FriendlyCharacterType = Parameter.CharacterParameters.FriendlyCharacterType;

namespace Character{
	public class Hero :IPlayable {
		/// <summary> キャラクターの個を表すID </summary>
		private long UNIQUE_ID;

		/// <summary> このキャラクターを取得しているContainerオブジェクト </summary>
		private Container container;

		/// <summary> このキャラクターの現在HP </summary>
		private int hp;
		/// <summary> このキャラクターの現在MP </summary>
		private int mp;
		/// <summary> このキャラクターの最大HP </summary>
		private int maxHp;
		/// <summary> このキャラクターの最大MP </summary>
		private int maxMp;

		/// <summary> 現在のゲームスコア </summary>
		private int score;
		/// <summary> キャラクターの取得経験値 </summary>
		private int exp;
		/// <summary> キャラクターのレベル </summary>
		private int level;

		/// <summary> BattleAbilityに登録されているパラメータのDictionary </summary>
		Dictionary<BattleAbility,int> battleAbilities = new Dictionary<BattleAbility, int>();
		/// <summary> FriendlyAbilityに登録されているパラメータのDictionary </summary>
		Dictionary<FriendlyAbility,int> friendlyAbilities = new Dictionary<FriendlyAbility, int>();

        /// <summary> このキャラクターの所持金 </summary>
        private int mt = 0;

        /// <summary> 使命達成を判定するフラグの記憶インスタンス </summary>
        private FlagList flags;

		/// <summary> キャラクターの職業 </summary>
		private Job job;
		/// <summary> このキャラクターの特徴 </summary>
		private Identity identity;
		/// <summary> このキャラクターの使命 </summary>
		private Mission mission;

		/// <summary> プレイヤーの所属派閥 </summary>
		private Faction faction = Faction.PLAYER;

		/// <summary> 装備中の武器 </summary>
        private Weapon weapon;
		/// <summary> 装備中の防具 </summary>
		private Armor armor;

        /// <summary> キャラクターが所持しているアイテム(keyをstring以外にする予定) </summary>
        private Inventry inventry = new Inventry();

		/// <summary> キャラクターが持つActiveSkillのリスト </summary>
		private List<IActiveSkill> activeSkills = new List<IActiveSkill>();
		/// <summary> キャラクターが持つReactionSkillのリスト </summary>
		private List<ReactionSkill> reactionSkills = new List<ReactionSkill>();

		/// <summary> このキャラクターがバトル中かどうか </summary>
		private bool isBattleing;
		/// <summary> （実装予定）カウンターを行うかどうか </summary>
		private bool isReadyToCounter;
		/// <summary> ボーナス値を管理するインスタンス </summary>
		private BonusKeeper bonusKeeper = new BonusKeeper();

		/// <summary> プレイヤーの苦手属性 </summary>
		private AttackSkillAttribute weakAttribute;

        private GameObject menuPrefab;

        private Party party = new Party();

        private List<IQuest> undertakingQuests = new List<IQuest>();

        /// <summary>
        /// <see cref="T:Character.Hero"/> classのコンストラクタです
        /// </summary>
        /// <param name="job">職業</param>
        /// <param name="con">Container</param>
        public Hero(Job job,Humanity humanity,List<Identity> identities, Container con){
			Dictionary<BattleAbility,int> battleParameters = job.defaultSettingBattleAbility ();
			Dictionary<FriendlyAbility,int> friendlyParameters = job.defaultSettingFriendlyAbility ();

			setMft (battleParameters[BattleAbility.MFT]);
			setFft (battleParameters [BattleAbility.FFT]);
			setMgp (battleParameters [BattleAbility.MGP]);
			setPhy (battleParameters [BattleAbility.PHY]);
			setAgi (battleParameters [BattleAbility.AGI]);
			setDex (friendlyParameters [FriendlyAbility.DEX]);
			setSpc (friendlyParameters [FriendlyAbility.SPC]);

			setMaxHp (battleAbilities[BattleAbility.PHY]);
			setMaxMp (battleAbilities[BattleAbility.MGP]);
            //かり
            setMaxHp(100);
            setMaxMp(100);
            hp = maxHp;
            mp = maxMp;

			this.container = con;

			UNIQUE_ID = UniqueIdCreator.creatUniqueId ();

			this.level = 2;

            menuPrefab = (GameObject)Resources.Load("Prefabs/Menu");

            party.join(this);

			//もうちとくふうするかも

			var bKeys = Enum.GetValues(typeof(BattleAbility));
			var fKeys = Enum.GetValues(typeof(FriendlyAbility));

			foreach (Identity identity in identities) {
                var battleBonus = identity.getBattleAbilityBonuses();
                foreach(BattleAbility ability in bKeys){
                    battleAbilities[ability] += battleBonus[ability];
                }

                var friBonus = identity.getFriendlyAblityBonuses();
                foreach (FriendlyAbility ability in fKeys){
                    friendlyAbilities[ability] += friBonus[ability];
                }

                identity.activateSkill(this);
            }

            humanity.activate(this);
            foreach(BattleAbility ability in bKeys){
                battleAbilities[ability] += humanity.getAbilityBonus(ability);
            }
            foreach (FriendlyAbility ability in fKeys) {
                friendlyAbilities[ability] += humanity.getAbilityBonus(ability);
			}

            checkAbilities();

            flags = new FlagList(this);
		}

        private void checkAbilities(){
			var bKeys = Enum.GetValues(typeof(BattleAbility));
			var fKeys = Enum.GetValues(typeof(FriendlyAbility));
			foreach (BattleAbility ability in bKeys) {
                if (battleAbilities[ability] < 0)
                    battleAbilities[ability] = 0;
			}
			foreach (FriendlyAbility ability in fKeys) {
                if (friendlyAbilities[ability] < 0)
                    friendlyAbilities[ability] = 0;
			}
        }

		#region IPlayable implementation
        public void equipWeapon (Weapon wepon) {
            if (this.weapon != null && this.weapon.getCanStore())
				addItem (this.weapon);
			this.weapon = wepon;
		}

        public void equipArmor (Armor armor) {
            if (this.armor != null && this.armor.getCanStore())
				addItem (this.armor);
			this.armor = armor;
		}

		public void levelUp () {
			throw new NotImplementedException ();
		}

		public void addExp (int val) {
			if (val >= 0)
				exp += val;
			throw new ArgumentException ("invalid argent in addExp()");
		}

		public int getExp () {
			return exp;
		}

        public Armor getArmor() {
            return armor;
        }

		public List<IActiveSkill> getActiveSkills () {
			return new List<IActiveSkill> (activeSkills);
		}


		public List<ReactionSkill> getReactionSKills () {
			return new List<ReactionSkill> (reactionSkills);

		}

		public void addSkill (IActiveSkill skill) {
			if (skill != null || !activeSkills.Contains (skill)) {
				activeSkills.Add (skill);
			} else {
				throw new ArgumentException ("invalid activeSkill");
			}
		}

		public void addSkill (ReactionSkill skill) {
			if (skill != null || !reactionSkills.Contains (skill)) {
				reactionSkills.Add (skill);
			} else {
				throw new ArgumentException ("invalid passiveSkill");
			}
		}

		public Container getContainer () {
			return this.container;
		}
		#endregion
		#region IFriendly implementation
        public int getFriendlyAbility(FriendlyAbility ability){
            return friendlyAbilities[ability];
        }

		public void talk (IFriendly friendly) {
			throw new NotImplementedException ();
		}
		#endregion
		#region IBattleable implementation
		public int getHp () {
			return hp;
		}

		public int getMp () {
			return mp;
		}

		public void dammage (int dammage, AttackSkillAttribute attribute) {
            if (dammage < 0)
                dammage = 0;
            
			if (attribute == AttackSkillAttribute.PHYSICAL)
				dammage -= getDef ();
			if(attribute == weakAttribute)
				dammage = (int)( dammage * 1.5f );
			
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
            if (heal < 0)
                heal = 0;


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

            hp = (hp > maxHp) ? hp = maxHp : hp;
            mp = (mp > maxMp) ? mp = maxMp : mp;
		}

        public int getRawAbility(BattleAbility ability){
            return battleAbilities[ability];
        }

		public int getAbilityContainsBonus(BattleAbility ability) {
            return battleAbilities[ability] + bonusKeeper.getBonus(ability);
		}

		public int getAtk (AttackSkillAttribute attribute, BattleAbility useAbility,bool useWepon) {
			int atk = battleAbilities [useAbility] + UnityEngine.Random.Range (0,level);
            if (useWepon)
                atk += this.weapon.attackWith();
			return atk;
		}

		/// <summary>
		/// 防御を取得
		/// 防御の計算式：装備品の防御 + (phy / 4 + mft / 4)
		/// </summary>
		/// <returns>The def.</returns>
		public int getDef () {
//			return armor.getDef() + (this.battleAbilities[BattleAbility.PHY]/4  + this.battleAbilities[BattleAbility.MFT]/4);
			return 0;
		}

		public float getCharacterDelay() {
            return weapon.getDelay();
		}

		public int getCharacterRange() {
            return weapon.getRange();
		}

		public BattleAbility getCharacterAttackMethod() {
            return weapon.getWeaponAbility();
		}

		public void addAbilityBonus(SubBattleAbilityBonus bonus) {
            bonusKeeper.setBonus(bonus);
		}

		public bool getIsBattling () {
			return isBattleing;
		}

		public void setIsBattling (bool boolean) {
			isBattleing = boolean;
		}

		public void syncronizePositioin (Vector3 vector) {
			container.getModel ().transform.position = vector;
		}

		public int getDodge() {
            return getAbilityContainsBonus(BattleAbility.AGI);
		}

		public int getHit (BattleAbility useAbility) {
            return getAbilityContainsBonus(useAbility) + UnityEngine.Random.Range (1,11);
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

		public int getMaxHp () {
			return maxHp;
		}

		public int getMaxMp () {
			return maxMp;
		}


		public int getHeal (BattleAbility useAbility) {
            return getAbilityContainsBonus(useAbility) + UnityEngine.Random.Range(0,level);
		}

		public Faction getFaction () {
			return faction;
		}

		public bool isHostility (Faction faction) {
			return (this.faction != faction);
		}
			
		public void encount () {
            if(!isBattleing)
    			BattleManager.getInstance().joinBattle(this,FieldPosition.ONE);
		}

		public void addAbilityBonus (BattleAbilityBonus bonus) {
            bonusKeeper.setBonus(bonus);
		}


		public void addSubAbilityBonus (SubBattleAbilityBonus bonus) {
            bonusKeeper.setBonus(bonus);
		}

		public Weapon getWeapon() {
			return weapon;
		}

		public FriendlyCharacterType getCharacterType() {
			return FriendlyCharacterType.PLAYABLE;
		}

        public int getId() {
            throw new InvalidOperationException("hero's getId is called");
        }
		#endregion
		#region ICharacter implementation

		public void act () {
            bonusKeeper.advanceLimit();

            if(Input.GetKeyDown(KeyCode.E)){
                GameObject menuObject = MonoBehaviour.Instantiate(menuPrefab,new Vector3(874f, 384f, 0f),new Quaternion(0,0,0,0));
                Menu menu = menuObject.GetComponent<Menu>();
                menu.transform.SetParent(CanvasGetter.getCanvas().transform);
                menu.setState(this,party);
            }
		}

		public void death () {
			Debug.Log ("game over!");
		}

		public long getUniqueId () {
			return UNIQUE_ID;
		}

		public string getName(){
			return "hero";
		}
		#endregion

		/// <summary>
        /// インベントリにアイテムを追加します
        /// </summary>
        /// <param name="item">追加するアイテム</param>
		public void addItem(IItem item){
            if (!item.getCanStore())
                throw new ArgumentException("item " + item.getName() +"can't be stored");
            inventry.addItem(item);
		}

        public Inventry getInventry(){
            return inventry;
        }

        public void undertake(IQuest quest){
            this.undertakingQuests.Add(quest);
        }

        public void deleteQuest(IQuest quest){
            this.undertakingQuests.Remove(quest);
        }

        public List<IQuest> getUndertakingQuests(){
            return new List<IQuest>(undertakingQuests);
        }

        public FlagList getFlagList(){
            return flags;
        }

		/// <summary>
        /// 最大HPを設定します
        /// </summary>
        /// <param name="parameter">設定するHP</param>
		private void setMaxHp(int parameter){
			isntInvalitAblityParamter (parameter);
			maxHp = parameter;
		}

		/// <summary>
        /// 最大MPを設定します
        /// </summary>
        /// <param name="parameter">設定するMP</param>
		private void setMaxMp(int parameter){
			isntInvalitAblityParamter (parameter);
			maxMp = parameter;
		}

		/// <summary>
        /// 白兵攻撃力(MFT)を設定します
        /// </summary>
        /// <param name="parameter">設定するパラメータ</param>
		private void setMft(int parameter){
			isntInvalitAblityParamter (parameter);
			this.battleAbilities[BattleAbility.MFT] = parameter;
		}

		/// <summary>
		/// 遠距離攻撃力(FFT)を設定します
		/// </summary>
		/// <param name="parameter">設定するパラメータ</param>
		private void setFft(int parameter){
			isntInvalitAblityParamter (parameter);
			this.battleAbilities[BattleAbility.FFT] = parameter;
		}

		/// <summary>
		/// 魔力(MGP)を設定します
		/// </summary>
		/// <param name="parameter">設定するパラメータ</param>
		private void setMgp(int parameter){
			isntInvalitAblityParamter (parameter);
			this.battleAbilities[BattleAbility.MGP] = parameter;
		}

		/// <summary>
		/// 話術(SPC)を設定します
		/// </summary>
		/// <param name="parameter">設定するパラメータ</param>
		private void setSpc(int parameter){
			isntInvalitAblityParamter(parameter);
			this.friendlyAbilities[FriendlyAbility.SPC] = parameter;
		}

		/// <summary>
		/// 器用(DEX)を設定します
		/// </summary>
		/// <param name="parameter">設定するパラメータ</param>
		private void setDex(int parameter){
			isntInvalitAblityParamter (parameter);
			this.friendlyAbilities[FriendlyAbility.DEX] = parameter;
		}

		/// <summary>
		/// 体力(PHY)を設定します
		/// </summary>
		/// <param name="parameter">設定するパラメータ</param>
		private void setPhy(int parameter){
			isntInvalitAblityParamter (parameter);
			this.battleAbilities[BattleAbility.PHY] = parameter;
		}

		/// <summary>
        /// 敏捷性(AGI)を設定します
        /// </summary>
        /// <param name="parameter">設定するパラメータ</param>
		private void setAgi(int parameter){
			isntInvalitAblityParamter (parameter);
			this.battleAbilities[BattleAbility.AGI] = parameter;
		}

		/// <summary>
        /// 与えられた値を検査し、不正な場合は例外を投げます
        /// </summary>
        /// <param name="value"> 検査したい値 </param>
		private void isntInvalitAblityParamter(int value){
			if (value <= 0)
				throw new ArgumentException ("invalit parameter");
		}

        public int getMetal(){
            return this.mt;
        }

        public void addMetal(int metal){
            this.mt += metal;
        }

        public void minusMetal(int metal){
            this.mt -= metal;
        }

		public override string ToString () {
			return "Hero No." + UNIQUE_ID;
		}
			
		public override bool Equals (object obj) {
			return this == obj;
		}

        public Dictionary<BattleAbility, int> getBattleAbilities() {
            return new Dictionary<BattleAbility, int>(battleAbilities);
        }

        public Dictionary<FriendlyAbility, int> getFriendlyAbilities() {
            return new Dictionary<FriendlyAbility, int>(friendlyAbilities);
        }

    }
}