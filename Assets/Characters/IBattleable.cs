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
    /// <summary>
    /// IBattleableは全てのバトル可能なキャラクターを表します
    /// </summary>
	public interface IBattleable : ICharacter{

		/// <summary>
        /// 現在のHPを取得します
        /// </summary>
        /// <returns>現在のHP</returns>
		int getHp();

		/// <summary>
        /// HPを減少させます
        /// </summary>
        /// <param name="dammage"> ダメージの数値 </param>
        /// <param name="attribute"> 攻撃の属性 </param>
		void dammage (int dammage,AttackSkillAttribute attribute);

		/// <summary>
        /// 回復されます(受動側)
        /// </summary>
        /// <param name="heal"> 回復値 </param>
        /// <param name="attribute"> 回復の属性 </param>
		void healed(int heal,HealSkillAttribute attribute);

		/// <summary>
        /// 現在のMPを取得します
        /// </summary>
        /// <returns>現在のMP</returns>
		int getMp();

		/// <summary>
		/// MPを減少させます
		/// </summary>
		/// <param name="value">減少値</param>
		void minusMp(int value);

		/// <summary>
		/// 最大のHPを取得します
		/// </summary>
		/// <returns>最大HP</returns>
		int getMaxHp();

		/// <summary>
		/// 最大MPを取得します
		/// </summary>
		/// <returns>最大MP</returns>
		int getMaxMp();

		/// <summary>
		/// 白兵戦闘能力(melee fighting / mft)を取得します
		/// </summary>
		/// <returns>白兵戦闘力</returns>
		int getMft();

		/// <summary>
		/// 遠距離戦闘能力(far fighting / fft)を取得します
		/// </summary>
		/// <returns>遠距離戦闘力</returns>
		int getFft();

		/// <summary>
		/// 魔力(magic power / mgp)を取得します
		/// </summary>
		/// <returns> 魔力 </returns>
		int getMgp();

		/// <summary>
		/// 敏捷性(agility / agi)を取得します
		/// </summary>
		/// <returns>敏捷性</returns>
		int getAgi();

		/// <summary>
		/// 体力(phycical / phy)を取得します
		/// </summary>
		/// <returns> 体力 </returns>
		int getPhy();

		/// <summary>
		/// ボーナス値を含んだ各能力値を取得します
		/// </summary>
		/// <returns>ボーナスを含んだ能力値</returns>
		/// <param name="ability">取得したい能力値</param>
		int getAbilityContainsBonus(BattleAbility ability);

		/// <summary>
        /// 攻撃力を取得します
        /// この返し値は武器の攻撃力は含みません
        /// getWeponを使用して武器を取得し、それのgetAtkを呼び出してください
        /// </summary>
        /// <returns>攻撃力</returns>
        /// <param name="attribute">攻撃の属性</param>
        /// <param name="useAbility">使用する能力値</param>
		int getAtk(AttackSkillAttribute attribute,BattleAbility useAbility,bool useWepon);

		/// <summary>
		/// 回復値を取得します(能動側)
		/// </summary>
		/// <returns> 回復値 </returns>
		/// <param name="useAbility">使用するBattleAbility</param>
		int getHeal(BattleAbility useAbility);

		/// <summary>
        /// 防御力を取得します
        /// この返し値は防具の防御力を含みます
        /// </summary>
        /// <returns>The def.</returns>
		int getDef();

		/// <summary>
		/// 攻撃の命中値を取得します
		/// </summary>
		/// <returns>命中値</returns>
		/// <param name="useAbility">使用するBattleAbility</param>
		int getHit(BattleAbility useAbility);

		/// <summary>
		/// 回避値を取得します
		/// </summary>
		/// <returns>回避値</returns>
		int getDodge();

        /// <summary>
        /// キャラクター自身(武器、能力値などによる)のディレイ値を取得します
        /// </summary>
        /// <returns> ディレイ値 </returns>
        float getCharacterDelay();

		/// <summary>
		/// キャラクター自身(武器、能力値などによる)の攻撃の射程を取得します
		/// </summary>
		/// <returns> 攻撃の射程 </returns>
		int getCharacterRange();

		/// <summary>
		/// キャラクター自身(武器、能力値などによる)の攻撃に使うBattleAbilityを取得します
		/// </summary>
		/// <returns> 攻撃に使うBattleAbility </returns>
		BattleAbility getCharacterAttackMethod();

		/// <summary>
		/// カウンターを行うかどうかのフラグを設定します
		/// 未実装です
		/// </summary>
		/// <param name="flag">行う場合に <c>true</c> を設定します</param>
		void setIsReadyToCounter(bool flag);

		/// <summary>
        /// 戦闘中かどうかを取得します
        /// </summary>
        /// <returns><c>true</c>, 戦闘中, <c>false</c> 戦闘中でない</returns>
		bool getIsBattling();

		/// <summary>
        /// 戦闘中かどうかのフラグを変更します
        /// </summary>
        /// <param name="boolean">戦闘中ならば <c>true</c> にします</param>
		void setIsBattling(bool boolean);

		/// <summary>
        /// Containerを与えられたポジションに同期します
        /// </summary>
        /// <param name="vector">同期したいポジション</param>
		void syncronizePositioin(Vector3 vector);

        /// <summary>
        /// BattleAbilityに対する能力値ボーナスを設定します
        /// </summary>
        /// <param name="bonus">設定したいボーナスインスタンス</param>
		void addAbilityBonus (BattleAbilityBonus bonus);

        /// <summary>
        /// SubBattleAbilityに対する能力値ボーナスを設定します
        /// </summary>
        /// <param name="bonus">設定したいボーナスインスタンス</param>
		void addAbilityBonus (SubBattleAbilityBonus bonus);

		/// <summary>
        /// ボーナスを無条件で全て解除します
        /// </summary>
		void resetBonus();

		/// <summary>
        /// レベルを取得します
        /// </summary>
        /// <returns>The level.</returns>
		int getLevel();

		/// <summary>
        /// 派閥を取得します
        /// </summary>
        /// <returns>派閥</returns>
		Faction getFaction();

		/// <summary>
        /// 敵対しているかを判定します
        /// </summary>
        /// <returns><c>true</c>, 敵対, <c>false</c> 友好</returns>
        /// <param name="faction">判定したい派閥</param>
		bool isHostility(Faction faction);

		/// <summary>
        /// エンカウントし、バトルに突入します
        /// </summary>
		void encount();
	}
}
