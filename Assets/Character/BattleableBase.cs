using character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using skill;
using item;
using battleSystem;

namespace character{
	//抽象クラスにつき、必ず仮想変数をオーバーライドしてください。
	public abstract class BattleableBase : Character{

		//HPを表します。必ず0 =< hp =< maxHpが成り立ちます。
		protected int hp;
		//最大HPを表します。必ず0より大きい数になります。
		protected int maxHp;
		//MPを表します。必ず0 =< mp =< maxMpが成り立ちます。
		protected int mp;
		//最大MPを表します。必ず0より大きい数になります。
		protected int maxMp;
		//白兵戦闘力(melee fighting)を表します。必ず0以上です。
		protected int mft;
		//遠戦闘力(far fighting)を表します。必ず0以上です。
		protected int fft;
		//魔力(magic power)を表します。必ず0以上です。
		protected int mgp;
		//敏捷性(agility)を表します。必ず0以上です。
		protected int agi;
		//体力(phygical)を表します。必ず0以上です。
		protected int phy;
		//レベルを表します。
		protected int level;
		//弱点属性を表します。
		protected readonly SkillType WEAK_TYPE;
		//戦闘状態であるかを表します。初期値はfalseです。
		protected bool isBattling = false;
		//防御に対しての外付けボーナスを表します。初期値は0です。
		protected int defBonus = 0;
		//回避に対しての外付けボーナスを表します。初期値は0です。
		protected int dodgeBonus = 0;
		//攻撃に対しての外付けボーナスを表します。初期値は0です。
		protected int atkBonus = 0;
		//カウンターを行うかを表します。初期値はfalseです。
		protected bool doCounter = false;

		public BattleableBase(BattleableBaseBuilder builder){
			
		}

		//HPを返します
		public int getHp(){
			return hp;
		}

		//HPを減少させます
		public void dammage (int dammage,SkillType type){
			//弱点属性は攻撃力1.5倍
			if(type == WEAK_TYPE)
				dammage = (int) dammage * 1.5f;
			setHp (getHp () - dammage);
			if (getHp () < 0)
				setHp (0);
		}

		//HPを設定します
		public void setHp(int hp){
			if (!(hp > maxHp || hp < 0))
				throw new ArgumentException ("wrong hp in battleableBase");
			this.hp = hp;
		}

		//MPを返します
		public int getMp(){
			return mp;
		}

		//MPを設定します
		public void setMp(int mp){
			if (!(mp > maxMp || 0 < mp))
				throw new ArgumentException ("wrong mp in battleableBase");
			this.mp = mp;
		}

		//白兵戦闘能力(melee fighting)を返します
		public int getMft(){
			return mft;
		}

		//遠戦闘能力(far fighting)を返します
		public int getFft(){
			return fft;
		}

		//魔力(magic power)を返します
		public int getMgp(){
			return mgp;
		}

		//敏捷性(agility)を返します
		public int getAgi(){
			return agi;
		}

		//体力(phygical)を返します
		public int getPhy(){
			return phy;
		}

		//戦闘中かどうかを表します
		public bool getIsBattling(){
			return isBattling;
		}

		//戦闘中かのフラグを切り替えます
		public void setIsBattling(bool boolean){
			isBattling = boolean;
		}

		//防御へのボーナスを設定します。isButtlingがtrue時のみ呼びだされます。
		public void setDefBonus(int bonus){
			this.defBonus = bonus;
		}

		//回避へのボーナスを設定します。isButtlingがtrue時のみ呼びだされます。
		public void setDodgeBonus(int bonus){
			this.dodgeBonus = bonus;
		}

		//攻撃へのボーナスを設定します。isButtlingがtrue時のみ呼びだされます。
		public void setAtkBonus(int bonus){
			this.atkBonus = bonus;
		}

		//カウンターを行うかどうかのフラグを設定します。isButtlingがtrue時のみ呼びだされます。
		public void setDoCounter(bool flag){
			doCounter = flag;
		}

		//ボーナスをリセットします。isButtlingがtrue時のみ呼びだされます。
		public void resetBonus(){
			dodgeBonus = 0;
			atkBonus = 0;
			defBonus = 0;
			doCounter = false;
		}

		//レベルを取得します
		public int getLevel(){
			return level;
		}

		//防御力(defence)を返します
		public abstract int getDef();

		//ディレイ値を返します
		public abstract float getDelay(ActiveSkill skill);

		//移動する量を決定します。isButtlingがtrue時のみ呼びだされます。
		public abstract int move();

		//containerの位置を現在の位置と同期させます
		public abstract void syncronizePositioin(Vector3 vector);

		//戦闘時の行動の決定を行います。isButtlingがtrue時のみ呼び出されます。
		public abstract BattleCommand decideCommand();

		//攻撃するSkillを決定し、そのスキルを返します。
		public abstract ActiveSkill decideSkill();

		//対象のスキルの射程を算出します。isButtlingがtrue時のみ呼びだされます。
		public abstract int getRange(ActiveSkill skill);

		//攻撃の対象を決定します。isButtlingがtrue時のみ呼び出されます。
		public abstract List<BattleableBase> decideTarget(List<BattleableBase> bals);

		//攻撃の成功値を算出します。isButtlingがtrueの時のみ呼び出されます。
		public abstract int getHitness(ActiveSkill skill);

		//攻撃やスキルを使用し、ダメージを返します。isButtlingがtrue時のみ呼び出されます。
		public abstract int battleAction(ActiveSkill skill);

		//受動の行動を決定します。isBattlingがtrue時のみ呼びだされます。
		public abstract PassiveSkill decidePassiveSkill();

		//回避の達成値を表します。基本的にisButtlingがtrue時のみ呼びだされます。
		public abstract int getDodgeNess();
	}
}

namespace battleSystem{
	public enum AttackType{MELEE,FAR,MAGIC};
}
