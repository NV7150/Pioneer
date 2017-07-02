using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Item;
using Skill;
using Parameter;
using BattleSystem;

namespace Character{
	public class Hero :IPlayable {
		//このキャラクターのHPを表します
		private int hp;
		//このキャラクターのMPを表します
		private int mp;
		//ゲームスコアを表します
		private int score;
		//キャタクターの経験値を表します
		private int exp;
		//キャラクターの各種パラメータを表します
		Dictionary<Ability,int> abilities = new Dictionary<Ability, int>();
		//このキャラクターの所持金(metal)を表します
		private int mt = 0;
		//使命達成用のflugリストです。
		private FlugList flugs;
		//このキャラクターの職業を表します
		private Job job;
		//このキャラクターの個性を表します
		private IIdentity identity;
		//このキャラクターの使命を表します
		private Mission mission;
		//このキャラクターが装備中の武器を表します
		private Wepon wepon;
		//このキャラクターが装備中の防具を返します
		private Armor armor;
		//このキャラクターのインベントリを表します
		private Dictionary<string,ItemStack> inventry = new Dictionary<string,ItemStack>();
		//このキャラクターがバトル中かを示します
		private bool isBattleing;
		//このキャラクターの実体を表します
		private Container container;
		//このキャラクターの防御へのボーナスを表します
		private int defBonus = 0;
		//このキャラクターの回避へのボーナスを表します
		private int dodgeBonus = 0;
		//このキャラクターの攻撃へのボーナスを表します
		private int atkBonus = 0;
		//カウンターを行うかを表します
		private bool isReadyToCounter;
		//プレイヤーの派閥を表します
		private Faction faction = Faction.PLAYER;
		//プレイヤーの苦手属性を表します
		private SkillAttribute weakAttribute;
		//このキャラクターのunipueIdを表します
		private long UNIQUE_ID;
		//このキャラクターが持つ能動スキルのリストです
		private List<ActiveSkill> activeSkills = new List<ActiveSkill>();
		//このキャラクターが持つ受動スキルのリストです
		private List<ReactionSkill> reactionSkills = new List<ReactionSkill>();


		public Hero(Job job,Container con){
			Dictionary<Ability,int> parameters = job.defaultSetting ();

			setMft (parameters[Ability.MFT]);
			setFft (parameters [Ability.FFT]);
			setMgp (parameters [Ability.MGP]);
			setPhy (parameters [Ability.PHY]);
			setAgi (parameters [Ability.AGI]);
			setDex (parameters [Ability.DEX]);
			setSpc (parameters [Ability.SPC]);
			abilities [Ability.LV] = 1;

			setMaxHp (abilities[Ability.PHY]);
			setMaxMp (abilities[Ability.MGP]);
			hp = 100;
			mp = abilities [Ability.MP];

			this.container = con;

			UNIQUE_ID = UniqueIdCreator.creatUniqueId ();
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

		public Wepon getWepon () {
			return wepon;
		}

		public Armor getArmor () {
			return armor;
		}

		public int getDex () {
			return abilities[Ability.DEX];
		}

		public List<ActiveSkill> getActiveSkills () {
			return new List<ActiveSkill> (activeSkills);
		}


		public List<ReactionSkill> getReactionSKills () {
			return new List<ReactionSkill> (reactionSkills);

		}

		public void addSkill (ActiveSkill skill) {
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
		public int getSpc () {
			return abilities[Ability.SPC];
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

		public void dammage (int dammage, SkillAttribute attribute) {
			if (dammage < 0 || attribute == SkillAttribute.NONE)
				throw new ArgumentException ("invlit dammage");

			if (attribute == SkillAttribute.PHYSICAL)
				dammage -= getDef ();
			if(attribute == weakAttribute)
				dammage = (int)( dammage * 1.5f );
			
			this.hp -= dammage;
		}


		public void healed (int heal, HealAttribute attribute) {
			if (heal < 0 || attribute == HealAttribute.NONE)
				throw new ArgumentException ("invlit heal");

			if (attribute == HealAttribute.HP_HEAL || attribute == HealAttribute.BOTH) {
				if (this.hp != 0)
					this.hp += heal;
			}
			if (attribute == HealAttribute.MP_HEAL || attribute == HealAttribute.BOTH) {
				this.mp += heal;
			}
			if (attribute == HealAttribute.RESURRECTITION) {
				if(this.hp == 0)
					this.hp += heal;
			}
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
			//もっと工夫しようず
			return 10;
		}

		public int getDef () {
//			return getMft () /  2 + armor.getDef (); 
			return 0;
		}

		public int getDelay () {
//			return getWepon ().getDelay ();
			//wepon実装まだなんで
			return 1;
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

		public int getHit (Ability useAbility) {
			return abilities [useAbility] + UnityEngine.Random.Range (1,11);
		}

		public int getDodge () {
			return getAgi();
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
			return abilities [Ability.LV];
		}

		public int getMaxHp () {
			return abilities[Ability.HP];
		}

		public int getMaxMp () {
			return abilities[Ability.MP];
		}

		public int attack (int baseParameter, Ability useAbility) {
			return baseParameter + abilities [useAbility];
		}

		public int healing (int baseParameter, Ability useAbility) {
			return baseParameter + abilities [useAbility];
		}

		public Faction getFaction () {
			return faction;
		}

		public bool isHostility (Faction faction) {
			return (this.faction == faction);
		}


			
		public void encount () {
			BattleManager.getInstance().joinBattle(this,FieldPosition.ONE);
		}
		#endregion
		#region ICharacter implementation
		public GameObject getModel () {
			return container.getModel ();
		}

		public void act () {
//			Debug.Log ("acted");
		}

		public void death () {
			throw new NotImplementedException ();
		}

		public long getUniqueId () {
			return UNIQUE_ID;
		}

		public string getName(){
			return "hero";
		}
		#endregion

		//インベントリにアイテムを追加します
		public void addItem(IItem item){
			if (inventry.ContainsKey (item.getName ())) {
				inventry [item.getName()].add (item);
			} else {
				ItemStack stack = new ItemStack ();
				stack.add (item);
				inventry [item.getName ()] = stack;
			}
		}

		//最大HPを設定します
		private void setMaxHp(int parameter){
			isntInvalitAblityParamter (parameter);
			this.abilities [Ability.HP] = parameter;
		}

		//最大MPを設定します
		private void setMaxMp(int parameter){
			isntInvalitAblityParamter (parameter);
			this.abilities [Ability.MP] = parameter;
		}

		//白兵戦闘力(mft)を設定します
		private void setMft(int parameter){
			isntInvalitAblityParamter (parameter);
			this.abilities[Ability.MFT] = parameter;
		}

		//遠距離戦闘能力(fft)を設定します
		private void setFft(int parameter){
			isntInvalitAblityParamter (parameter);
			this.abilities[Ability.FFT] = parameter;
		}

		//魔力(mgp)を設定します
		private void setMgp(int parameter){
			isntInvalitAblityParamter (parameter);
			this.abilities[Ability.MGP] = parameter;
		}

		//話術(spc)を設定します
		private void setSpc(int parameter){
			isntInvalitAblityParamter(parameter);
			this.abilities[Ability.SPC] = parameter;
		}

		//器用さ(dex)を設定します
		private void setDex(int parameter){
			isntInvalitAblityParamter (parameter);
			this.abilities[Ability.DEX] = parameter;
		}

		//体力(phy)を設定します
		private void setPhy(int parameter){
			isntInvalitAblityParamter (parameter);
			this.abilities[Ability.PHY] = parameter;
		}

		//敏捷性(agi)を設定します
		private void setAgi(int parameter){
			isntInvalitAblityParamter (parameter);
			this.abilities[Ability.AGI] = parameter;
		}

		//与えられた値を検査し、不正な値の場合は例外を投げます
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