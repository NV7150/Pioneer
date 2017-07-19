using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using BattleSystem;

using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;
using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using Extent = Skill.ActiveSkillParameters.Extent;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Skill {
	public class AttackSkill : IActiveSkill{
		private readonly int 
			/// <summary> このスキルのID </summary>
			ID,
			/// <summary> このスキルの攻撃力 </summary>
			ATK,
			/// <summary> このスキルの射程 </summary>
			RANGE,
			/// <summary> このスキルの命中率 </summary>
			HIT,
			/// <summary> このスキルのMPコスト </summary>
			COST;

		private readonly string
			/// <summary> このスキルの名称 </summary>
			NAME,
			/// <summary> このスキルの説明文 </summary>
			DESCRIPTON,
            /// <summary> このスキルのフレーバーテキスト </summary>
            FLAVOR_TEXT;

		private readonly bool
			/// <summary> 命中率が武器に依存するかのフラグ </summary>
			DEPEND_HIT,
			/// <summary> 攻撃力が武器に依存するかのフラグ </summary>
			DEPEND_ATK,
			/// <summary> 射程が武器に依存するかのフラグ </summary>
			DEPEND_RANGE,
			/// <summary> ディレイ値が武器に依存するかのフラグ </summary>
			DEPEND_DELAY,
			/// <summary> 使用する能力値が武器に依存するかのフラグ </summary>
			DEPEND_ABILITY;

		/// <summary> ディレイ秒数 </summary>
		private readonly float DELAY;

		/// <summary> このスキルの属性 </summary>
		private readonly AttackSkillAttribute ATTRIBUTE;

		/// <summary> このスキルの判定に使用する能力値 </summary>
		private BattleAbility USE_ABILITY;

		/// <summary> このスキルの効果範囲 </summary>
		private readonly Extent EXTENT;

		public AttackSkill (string[] datas) {
			ID = int.Parse(datas [0]);
			NAME = datas [1];
			ATK = int.Parse(datas [2]);
			RANGE = int.Parse(datas [3]);
			HIT = int.Parse(datas [4]);
			DELAY = float.Parse(datas [5]);
			COST = int.Parse (datas[6]);
			DEPEND_ATK  = (datas[7] == "TRUE");
			DEPEND_HIT = (datas [8] == "TRUE");
			DEPEND_RANGE = (datas [9] == "TRUE");
			DEPEND_DELAY = (datas[10] == "TRUE");
			ATTRIBUTE = (AttackSkillAttribute)Enum.Parse(typeof(AttackSkillAttribute), datas[11]);

			if (datas[12] == "DEPEND") {
				DEPEND_ABILITY = true;
			} else {
				USE_ABILITY = (BattleAbility)Enum.Parse (typeof(BattleAbility), datas [12]);
			}

			EXTENT = (Extent)Enum.Parse (typeof(Extent),datas[13]);
			DESCRIPTON = datas [14];
            FLAVOR_TEXT = datas[15];
		}


		/// <summary>
		/// 攻撃を行います
		/// </summary>
		/// <param name="bal"> 攻撃を行うIBattleableキャラクター </param>
		/// <param name="targets"> 攻撃の対象のリスト </param>
		private void attack(IBattleable bal,List<IBattleable> targets){
			if (targets.Count <= 0)
				throw new InvalidOperationException ("invlid battleTask operation");

			foreach (IBattleable target in targets) {
                //対象のリアクション
                IBattleTaskManager targetManager = BattleManager.getInstance().getTaskManager(target.getUniqueId());
                targetManager.offerReaction(bal, this);
			}
		}

		/// <summary>
		/// 攻撃力を取得します
		/// </summary>
		/// <returns> 攻撃力 </returns>
		public int getAtk(IBattleable actioner){
            return ATK + actioner.getAtk (getAttackSkillAttribute(),getUseAbility(actioner),DEPEND_ATK);
		}

		/// <summary>
		/// 命中力を取得します
		/// </summary>
		/// <returns> 命中力 </returns>
		public int getHit(IBattleable actioner){
			int bonus = 0;
			if (DEPEND_HIT) {
//				bonus += actioner.getWepon ().getHit() ;
			}
			return HIT + actioner.getHit(getUseAbility(actioner)) + bonus;
		}

		/// <summary>
		/// このスキルの属性を取得します
		/// </summary>
		/// <returns> 属性 </returns>
		public AttackSkillAttribute getAttackSkillAttribute(){
			return this.ATTRIBUTE;
		}

		/// <summary>
		/// 判定に使う能力値を取得します
		/// </summary>
		/// <returns> 判定に使う能力値 使用者依存ならNONEを返します </returns>
		public BattleAbility getUseAbility(IBattleable actoiner){
			if (DEPEND_ABILITY) {
                return actoiner.getCharacterAttackMethod();
			} else {
				return this.USE_ABILITY;
			}
		}

		/// <summary>
		/// スキルの効果範囲を取得します
		/// </summary>
		/// <returns> 効果範囲 </returns>
		public Extent getExtent(){
			return this.EXTENT;
		}
			
		/// <summary>
		/// 射程を取得します
		/// </summary>
		/// <returns> 射程 </returns>
		/// <param name="actioner"> 射程を算出したいIBattleableキャラクター </param>
		public int getRange(IBattleable actioner){
			int bonus = 0;
			if (DEPEND_RANGE) {
                bonus += actioner.getCharacterRange();
			}
			return this.RANGE + bonus; 
		}


		#region IActiveSkill implementation

		public void action (IBattleable actioner,BattleTask task) {
            if (!ActiveSkillSupporter.canUseAffectSkill(actioner, task.getTargets(), this))
                return;

			attack (actioner,task.getTargets());

			actioner.minusMp (this.COST);
		}
			
		public int getCost () {
			return COST;
		}

		public float getDelay(IBattleable actioner){
            float bonus = 0;
			if (DEPEND_DELAY) {
                bonus += actioner.getCharacterDelay();
			}
			return this.DELAY + bonus;
		}

		public ActiveSkillType getActiveSkillType () {
			return ActiveSkillType.ATTACK;
		}

		public bool isFriendly () {
			return false;
		}
		#endregion

		#region ISkill implementation

		public string getName () {
			return this.NAME;
		}

		public string getDescription () {
			return this.DESCRIPTON;
		}

		public string getFlavorText() {
			throw new NotImplementedException();
		}

		public int getId () {
			return this.ID;
		}

		#endregion

		public override string ToString () {
			return "AttackSkill id:" + ID + " name:" + NAME;
		}


    }
}

