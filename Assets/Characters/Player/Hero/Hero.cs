using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Item;
using Skill;
using Parameter;
using BattleSystem;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using FriendlyAbility = Parameter.CharacterParameters.FriendlyAbility;
using Faction = Parameter.CharacterParameters.Faction;
using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using HealSkillAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;

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
		private FlugList flugs;

		/// <summary> キャラクターの職業 </summary>
		private Job job;
		/// <summary> このキャラクターの特徴 </summary>
		private IIdentity identity;
		/// <summary> このキャラクターの使命 </summary>
		private Mission mission;

		/// <summary> プレイヤーの所属派閥 </summary>
		private Faction faction = Faction.PLAYER;

		/// <summary> 装備中の武器 </summary>
		private Wepon wepon;
		/// <summary> 装備中の防具 </summary>
		private Armor armor;

		/// <summary> キャラクターが所持しているアイテム(keyをstring以外にする予定) </summary>
		private Dictionary<string,ItemStack> inventry = new Dictionary<string,ItemStack>();

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

        /// <summary>
        /// <see cref="T:Character.Hero"/> classのコンストラクタです
        /// </summary>
        /// <param name="job">職業</param>
        /// <param name="con">Container</param>
		public Hero(Job job,Container con){
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
		}

		#region IPlayable implementation
		public bool equipWepon (Wepon wepon) {
			if (wepon.canEquip(this)) {
				if (this.wepon != null)
					addItem (this.wepon);
				this.wepon = wepon;
				return true;
			} else {
				return false;
			}

		}

		public bool equipArmor (Armor armor) {
			if (armor.canEquip (this)) {
				if (this.armor != null)
					addItem (this.armor);
				this.armor = armor;
				return true;
			} else {
				return false;
			}
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
        public int getRawFriendlyAbility(FriendlyAbility ability){
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

			if (attribute == AttackSkillAttribute.PHYSICAL)
				dammage -= getDef ();
			if(attribute == weakAttribute)
				dammage = (int)( dammage * 1.5f );
			
			this.hp -= dammage;

			if (this.hp < 0)
				this.hp = 0;
		}

		public void minusMp (int value) {
			if (value > 0)
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

        public int getRawAbility(BattleAbility ability){
            return battleAbilities[ability];
        }

		public int getAbilityContainsBonus(BattleAbility ability) {
            return battleAbilities[ability] + bonusKeeper.getBonus(ability);
		}

		public int getAtk (AttackSkillAttribute attribute, BattleAbility useAbility,bool useWepon) {
			int atk = battleAbilities [useAbility] + UnityEngine.Random.Range (0,level);
            if (useWepon)
                atk += this.wepon.getAttack();
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
			throw new NotImplementedException();
		}

		public int getCharacterRange() {
			throw new NotImplementedException();
		}

		public BattleAbility getCharacterAttackMethod() {
			throw new NotImplementedException();
		}

		public void addAbilityBonus(SubBattleAbilityBonus bonus) {
			throw new NotImplementedException();
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

		public Wepon getWepon() {
			return wepon;
		}

		#endregion
		#region ICharacter implementation

		public void act () {
            bonusKeeper.advanceLimit();
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
			if (inventry.ContainsKey (item.getName ())) {
				inventry [item.getName()].add (item);
			} else {
				ItemStack stack = new ItemStack ();
				stack.add (item);
				inventry [item.getName ()] = stack;
			}
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

		public override string ToString () {
			return "Hero No." + UNIQUE_ID;
		}
			
		public override bool Equals (object obj) {
			return this == obj;
		}

       
    }
}