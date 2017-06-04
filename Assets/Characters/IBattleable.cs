using character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using skill;
using item;
using battleSystem;

namespace character{
	public interface IBattleable : ICharacter{

		//HPを返します
		int getHp();

		//HPを減少させます
		void dammage (int dammage,SkillType type);

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

		//防御力(defence)を返します
		int getDef();

		//ディレイ値を返します
		float getDelay(IActiveSkill skill);

		//戦闘中かどうかを表します
		bool getIsBattling();

		//戦闘中かのフラグを切り替えます
		void setIsBattling(bool boolean);

		//移動する量を決定します。isButtlingがtrue時のみ呼びだされます。
		int move();

		//containerの位置を現在の位置と同期させます
		void syncronizePositioin(Vector3 vector);

		//戦闘時の行動の決定を行います。isButtlingがtrue時のみ呼び出されます。
		BattleCommand decideCommand();

		//攻撃するSkillを決定し、そのスキルを返します。
		IActiveSkill decideSkill();

		//対象のスキルの射程を算出します。isButtlingがtrue時のみ呼びだされます。
		int getRange(IActiveSkill skill);

		//攻撃の対象を決定します。isButtlingがtrue時のみ呼び出されます。
		List<IBattleable> decideTarget(List<IBattleable> bals);

		//攻撃の成功値を算出します。isButtlingがtrueの時のみ呼び出されます。
		int getHitness(IActiveSkill skill);

		//攻撃やスキルを使用し、ダメージを返します。isButtlingがtrue時のみ呼び出されます。
		int battleAction(IActiveSkill skill);

		//受動の行動を決定します。isButtlingがtrue時のみ呼びだされます。
		IPassiveSkill decidePassiveSkill();

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
	}
}
