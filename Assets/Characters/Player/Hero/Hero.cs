
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using item;
using skill;
using parameter;
using battleSystem;

using System;

namespace character{
	public class Hero :IPlayable {
		//このキャラクターのHPを表します
		private int hp;
		//このキャラクターの最大HPを表します
		private int maxHp;
		//このキャラクターのMPを表します
		private int mp;
		//このキャラクターの最大MPを表します
		private int maxMp;
		//ゲームスコアを表します
		private int score;
		//キャラクターのレベルを表します
		private int level;
		//キャタクターの経験値を表します
		private int exp;
		//キャラクターの白兵戦闘能力を表します(melee fighting)
		private int mft;
		//このキャラクターの遠戦闘能力を表します(far fighting)
		private int fft;
		//このキャラクターの魔力を表します(magic power)
		private int mgp;
		//このキャラクターの敏捷性を表します(agility)
		private int agi;
		//このキャラクターの体力を表します(physical)
		private int phy;
		//このキャラクターの器用さを表します(dexerity)
		private int dex;
		//このキャラクターの話術を表します(speech)
		private int spc;
		//このキャラクターの所持金(metal)を表します
		private int mt;
		//使命達成用のflugリストです。
		private FlugList flugs;
		//このキャラクターの職業を表します
		private IJob job;
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

		public Hero(Dictionary<Ability,int> parameters){
			setMaxHp (parameters[Ability.HP]);
			setMaxMp (parameters [Ability.MP]);
			setMft (parameters[Ability.MFT]);
			setFft (parameters [Ability.FFT]);
			setMgp (parameters [Ability.MGP]);
			setPhy (parameters [Ability.PHY]);
			setAgi (parameters [Ability.AGI]);
			setDex (parameters [Ability.DEX]);
			setSpc (parameters [Ability.SPC]);
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
			return dex;
		}
		#endregion
		#region IFriendly implementation
		public int getSpc () {
			return spc;
		}

		public void talk (IFriendly friendly) {
			throw new NotImplementedException ();
		}
		#endregion
		#region IBattleable implementation
		public int getHp () {
			return hp;
		}

		public void dammage (int dammage, SkillType type) {
			throw new NotImplementedException ();
		}

		public int getMp () {
			return mp;
		}

		public void setHp (int hp) {
			if (hp > 0)
				this.hp = hp;
		}

		public void setMp (int mp) {
			if (mp > 0)
				this.mp = mp;
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
			return getMft () / 2 + armor.getDef (); 
		}

		public bool getIsBattling () {
			return isBattleing;
		}

		public void setIsBattling (bool boolean) {
			isBattleing = boolean;
		}

		public int move () {
			throw new NotImplementedException ();
		}

		public void syncronizePositioin (Vector3 vector) {
			container.getModel ().transform.position = vector;
		}

		public BattleCommand decideCommand () {
			throw new NotImplementedException ();
		}

		public ActiveSkill decideSkill () {
			throw new NotImplementedException ();
		}

		public List<IBattleable> decideTarget (List<IBattleable> bals) {
			throw new NotImplementedException ();
		}

		public int getHitness (ActiveSkill skill) {
			throw new NotImplementedException ();
		}

		public int battleAction (ActiveSkill skill) {
			throw new NotImplementedException ();
		}

		public IPassiveSkill decidePassiveSkill () {
			throw new NotImplementedException ();
		}

		public int getDodgeness () {
			return agi;
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

		public int getMaxHp () {
			return maxHp;
		}

		public int getMaxMp () {
			return maxMp;
		}

		public void dammage (int dammage, SkillAttribute attribute) {
			throw new NotImplementedException ();
		}


		public void healed (int heal, HealAttribute attribute) {
			throw new NotImplementedException ();
		}


		public float getDelay (float delay) {
			throw new NotImplementedException ();
		}


		public int move (int moveAmount) {
			throw new NotImplementedException ();
		}


		public int getRange (int range) {
			throw new NotImplementedException ();
		}


		public int getHitness (int hitness) {
			throw new NotImplementedException ();
		}


		public int attack (int baseParameter, Ability useAbility) {
			throw new NotImplementedException ();
		}


		public int healing (int baseParameter, Ability useAbility) {
			throw new NotImplementedException ();
		}

		#endregion
		#region ICharacter implementation
		public GameObject getModel () {
			return container.getModel ();
		}
		public void act () {
			throw new NotImplementedException ();
		}
		public void death () {
			throw new NotImplementedException ();
		}
		#endregion

		public void addItem(IItem item){
			if (inventry.ContainsKey (item.getName ())) {
				inventry [item.getName()].add (item);
			} else {
				ItemStack stack = new ItemStack ();
				stack.add (item);
				inventry [item.getName ()] = stack;
			}
		}
		private void setMaxHp(int parameter){
			if(parameter > 0)
				maxHp = parameter;
		}
		private void setMaxMp(int parameter){
			if (parameter > 0)
				maxMp = parameter;
		}
		private void setMft(int parameter){
			if (isntInvalitAblityParamter (parameter))
				mft = parameter;
			throw new ArgumentException ("invalit parameter");
		}
		private void setFft(int parameter){
			if (isntInvalitAblityParamter (parameter))
				fft = parameter;
			throw new ArgumentException ("invalit parameter");
		}
		private void setMgp(int parameter){
			if (isntInvalitAblityParamter(parameter))
				mgp = parameter;
			throw new ArgumentException ("invalit parameter");
		}
		private void setSpc(int parameter){
			if (isntInvalitAblityParamter(parameter))
				spc = parameter;
			throw new ArgumentException ("invalit parameter");
		}
		private void setDex(int parameter){
			if (isntInvalitAblityParamter(parameter))
				dex = parameter;
			throw new ArgumentException ("invalit parameter");
		}
		private void setPhy(int parameter){
			if (isntInvalitAblityParamter (parameter))
				phy = parameter;
			throw new ArgumentException ("invalit parameter");
		}
		private void setAgi(int parameter){
			if (isntInvalitAblityParamter (parameter))
				agi = parameter;
			throw new ArgumentException ("invalit parameter");
		}
		private bool isntInvalitAblityParamter(int value){
			return value >= 0;
		}

		public override bool Equals (object obj) {
			return this == obj;
		}
	}
}