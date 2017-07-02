﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using Item;
using BattleSystem;
using Parameter;
using Character;

namespace Character{
	public interface IBattleable : ICharacter{

		//HPを返します
		int getHp();

		//HPを減少させます
<<<<<<< HEAD
		void dammage (int dammage,ActiveSkillParameters.AttackSkillAttribute attribute);

		//回復されます（受動側）
		void healed(int heal,ActiveSkillParameters.HealSkillAttribute attribute);
=======
		void dammage (int dammage,SkillAttribute attribute);

		//回復されます（受動側）
		void healed(int heal,HealAttribute attribute);
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。

		//MPを返します
		int getMp();

		//HPを設定します
		void setHp(int hp);

		//MPを設定します
		void setMp(int mp);

		//最大HPを返します
		int getMaxHp();

		//最大MPを返します
		int getMaxMp();

		//白兵戦闘能力(melee fighting)を返します
		int getMft();

		//遠戦闘能力(far fighting)を返します
		int getFft();

		//魔力(magic power)を返します
		int getMgp();

		//敏捷性(agility)を返します
		int getAgi();

		//体力(phygical)を返します
		int getPhy();

		//攻撃力(atk)を返します。属性と使用能力値が必須です
<<<<<<< HEAD
		int getAtk(ActiveSkillParameters.AttackSkillAttribute attribute,CharacterParameters.Ability useAbility);
=======
		int getAtk(SkillAttribute attribute,Ability useAbility);

>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。

		//防御力(defence)を返します
		int getDef();

		//ディレイ値を返します
		float getDelay();

		//戦闘中かどうかを表します
		bool getIsBattling();

		//戦闘中かのフラグを切り替えます
		void setIsBattling(bool boolean);

		//containerの位置を現在の位置と同期させます
		void syncronizePositioin(Vector3 vector);

		//攻撃するSkillを決定し、そのスキルを返します。
		ActiveSkill decideSkill();

		//攻撃の対象を決定します。isButtlingがtrue時のみ呼び出されます。
		List<IBattleable> decideTarget(List<IBattleable> bals);

		//攻撃の成功値を算出します。isButtlingがtrueの時のみ呼び出されます。
<<<<<<< HEAD
		int getHit(CharacterParameters.Ability useAbility);
=======
		int getHitness(Ability useAbility);

		//受動の行動を決定します。isButtlingがtrue時のみ呼びだされます。
		PassiveSkill decidePassiveSkill();
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。

		//回避の達成値を表します。基本的にisButtlingがtrue時のみ呼びだされます。
		int getDodgeness();

		//防御へのボーナスを設定します。isButtlingがtrue時のみ呼びだされます。
		void setDefBonus(int bonus);

		//回避へのボーナスを設定します。isButtlingがtrue時のみ呼びだされます。
		void setDodBonus(int bonus);

		//攻撃へのボーナスを設定します。isButtlingがtrue時のみ呼びだされます。
		void setAtkBonus(int bonus);

		//カウンターを行うかどうかのフラグを設定します。isButtlingがtrue時のみ呼びだされます。
		void setIsReadyToCounter(bool flag);

		//ボーナスをリセットします。isButtlingがtrue時のみ呼びだされます。
		void resetBonus();

		//レベルを取得します
		int getLevel();

		//攻撃を行います
<<<<<<< HEAD
		int attack(int baseParameter,CharacterParameters.Ability useAbility);

		//回復を行います（能動側）
		int healing(int baseParameter,CharacterParameters.Ability useAbility);

		//派閥を取得します
		CharacterParameters.Faction getFaction();

		//敵対派閥かを取得します
		bool isHostility(CharacterParameters.Faction faction);
=======
		int attack(int baseParameter,Ability useAbility);

		//回復を行います（能動側）
		int healing(int baseParameter,Ability useAbility);

		//派閥を取得します
		Faction getFaction();

		//敵対派閥かを取得します
		bool isHostility(Faction faction);

		//名前を取得します
		string getName();
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。

		//エンカウントし、バトルに突入します
		void encount();
	}
}
