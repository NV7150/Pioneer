using System.Collections;
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
<<<<<<< HEAD
		void dammage (int dammage,ActiveSkillParameters.AttackSkillAttribute attribute);

		//回復されます（受動側）
		void healed(int heal,ActiveSkillParameters.HealSkillAttribute attribute);
=======
		void dammage (int dammage,SkillAttribute attribute);

		//回復されます（受動側）
		void healed(int heal,HealAttribute attribute);
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。
=======
		void dammage (int dammage,SkillParameters.SkillAttribute attribute);

		//回復されます（受動側）
		void healed(int heal,SkillParameters.HealAttribute attribute);
>>>>>>> cfdbb9b19b7aff48b2537cc983d1f41f037f910b

		//MPを返します
		int getMp();

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
<<<<<<< HEAD
		int getAtk(ActiveSkillParameters.AttackSkillAttribute attribute,CharacterParameters.Ability useAbility);
=======
		int getAtk(SkillAttribute attribute,Ability useAbility);

>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。
=======
		int getAtk(SkillParameters.SkillAttribute attribute,EnumParameters.Ability useAbility);
>>>>>>> cfdbb9b19b7aff48b2537cc983d1f41f037f910b

		//防御力(defence)を返します
		int getDef();

		//ディレイ値を返します
		int getDelay();

		//戦闘中かどうかを表します
		bool getIsBattling();

		//戦闘中かのフラグを切り替えます
		void setIsBattling(bool boolean);

		//containerの位置を現在の位置と同期させます
		void syncronizePositioin(Vector3 vector);

		//攻撃の成功値を算出します。isButtlingがtrueの時のみ呼び出されます。
<<<<<<< HEAD
<<<<<<< HEAD
		int getHit(CharacterParameters.Ability useAbility);
=======
		int getHitness(Ability useAbility);

		//受動の行動を決定します。isButtlingがtrue時のみ呼びだされます。
		PassiveSkill decidePassiveSkill();
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。
=======
		int getHit(EnumParameters.Ability useAbility);
>>>>>>> cfdbb9b19b7aff48b2537cc983d1f41f037f910b

		//回避の達成値を表します。基本的にisButtlingがtrue時のみ呼びだされます。
		int getDodge();

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
=======
		int attack(int baseParameter,EnumParameters.Ability useAbility);
>>>>>>> cfdbb9b19b7aff48b2537cc983d1f41f037f910b

		//回復を行います（能動側）
		int healing(int baseParameter,EnumParameters.Ability useAbility);

		//派閥を取得します
		EnumParameters.Faction getFaction();

		//敵対派閥かを取得します
<<<<<<< HEAD
		bool isHostility(Faction faction);

		//名前を取得します
		string getName();
>>>>>>> parent of cfdbb9b... コードの整理・ファイル構造の変更・いらない部分の削除などを行いました。
=======
		bool isHostility(EnumParameters.Faction faction);
>>>>>>> cfdbb9b19b7aff48b2537cc983d1f41f037f910b

		//エンカウントし、バトルに突入します
		void encount();
	}
}
