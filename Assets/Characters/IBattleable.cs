using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using Item;
using BattleSystem;
using Parameter;
using Character;

using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using HealSkillAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;
using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using Faction = Parameter.CharacterParameters.Faction;

namespace Character{
	public interface IBattleable : ICharacter{

		//HPを返します
		int getHp();

		//HPを減少させます
		void dammage (int dammage,AttackSkillAttribute attribute);

		//MPを減少させます
		void minusMp(int value);

		//回復されます（受動側）
		void healed(int heal,HealSkillAttribute attribute);

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
		int getAtk(AttackSkillAttribute attribute,BattleAbility useAbility);

		//防御力(defence)を返します
		int getDef();

		//戦闘中かどうかを表します
		bool getIsBattling();

		//戦闘中かのフラグを切り替えます
		void setIsBattling(bool boolean);

		//containerの位置を現在の位置と同期させます
		void syncronizePositioin(Vector3 vector);

		//攻撃の成功値を算出します。isButtlingがtrueの時のみ呼び出されます。
		int getHit(BattleAbility useAbility);

		//回避の達成値を表します。基本的にisButtlingがtrue時のみ呼びだされます。
		int getDodge();

		//カウンターを行うかどうかのフラグを設定します。isButtlingがtrue時のみ呼びだされます。
		void setIsReadyToCounter(bool flag);

		void addAbilityBonus (BattleAbilityBonus bonus);

		void addSubAbilityBonus (SubBattleAbilityBonus bonus);

		//ボーナスをリセットします。isButtlingがtrue時のみ呼びだされます。
		void resetBonus();

		//レベルを取得します
		int getLevel();

		//回復を行います（能動側）
		int healing(BattleAbility useAbility);

		//派閥を取得します
		Faction getFaction();

		//敵対派閥かを取得します
		bool isHostility(Faction faction);

		//エンカウントし、バトルに突入します
		void encount();

		//装備中の武器を返します
		Wepon getWepon();
	}
}
