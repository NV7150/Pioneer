using System;
using System.Collections;
using System.Collections.Generic;

using BattleSystem;
using Character;

using Extent = Skill.ActiveSkillParameters.Extent;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Skill {
    /// <summary>
    /// ActiveSkill系のクラス組むまでもない面倒な処理をまとめたクラスです
    /// </summary>
	public static class ActiveSkillSupporter {

		/// <summary>
		/// 与えられたIActiveSkillからExtentプロパティを取得します
		/// </summary>
		/// <returns> 検出したExtent ない場合はNONEを返します </returns>
		/// <param name="skill"> 検索したいIActiveSkill </param>
		public static Extent searchExtent(IActiveSkill skill){
			switch (skill.getActiveSkillType ()) {
				case ActiveSkillType.ATTACK:
					AttackSkill attackSkill = (AttackSkill)skill;
					return attackSkill.getExtent ();

				case ActiveSkillType.HEAL:
					HealSkill healSkill = (HealSkill)skill;
					return healSkill.getExtent ();

				case ActiveSkillType.BUF:
					BufSkill bufSkill = (BufSkill)skill;
					return bufSkill.getExtent ();

				case ActiveSkillType.DEBUF:
					DebufSkill debufSkill = (DebufSkill)skill;
					return debufSkill.getExtent();
			}
			throw new ArgumentException ("invalid skill " + skill);
		}

		/// <summary>
		/// 与えられたIAciveSkillからrangeを検出します
		/// </summary>
		/// <returns> 検出したrange</returns>
		/// <param name="skill"> 検索したいIActiveSkill </param>
		public static int searchRange(IActiveSkill skill,IBattleable attacker){
			switch (skill.getActiveSkillType ()) {
				case ActiveSkillType.ATTACK:
					AttackSkill attackSkill = (AttackSkill)skill;
					return attackSkill.getRange (attacker);

				case ActiveSkillType.HEAL:
					HealSkill healSkill = (HealSkill)skill;
					return healSkill.getRange ();

				case ActiveSkillType.BUF:
					BufSkill bufSkill = (BufSkill)skill;
					return bufSkill.getRange();

				case ActiveSkillType.DEBUF:
					DebufSkill debufSkill = (DebufSkill)skill;
					return debufSkill.getRange();
			}

			throw new ArgumentException ("invalid skill " + skill);
		}

		/// <summary>
		/// 渡されたIActiveSkillからmoveを検出します
		/// </summary>
		/// <returns> 検出したmove</returns>
		/// <param name="skill"> 検索したいIActiveSkill </param>
		public static int searchMove(IActiveSkill skill,IBattleable actoiner){
			if (skill.getActiveSkillType () == ActiveSkillType.MOVE) {
				MoveSkill moveSkill = (MoveSkill)skill;
				return moveSkill.getMove (actoiner);
			} else {
				throw new ArgumentException ("invalid skill " + skill);
			}
		}

        /// <summary>
        /// 対象がAffectSkillかを判定します
        /// AffectSkillとは、IActiveSkillの中でも、スキル対象が存在するスキルをさします
        /// 例えば、MoveSkillは対象が存在しないため、AffectSkillではありません
        /// </summary>
        /// <returns><c>true</c>, AffectSkill <c>false</c> AffectSkillでない</returns>
        /// <param name="skill">判定したいスキル</param>
        public static bool isAffectSkill(IActiveSkill skill) {
            ActiveSkillType type = skill.getActiveSkillType();
            bool needsTarget = (
                type == ActiveSkillType.ATTACK ||
                type == ActiveSkillType.BUF ||
                type == ActiveSkillType.DEBUF ||
                type == ActiveSkillType.HEAL
            );
            return needsTarget;
        }

        /// <summary>
        /// 与えられたAffectSkillが、その使用者によって対象に使用できるかを判定します
        /// </summary>
        /// <returns><c>true</c>, 使用可能, <c>false</c> 使用不可</returns>
        /// <param name="actioner">スキル使用者</param>
        /// <param name="targets">スキル対象</param>
        /// <param name="skill">使用するスキル　AffectSkillである必要があります</param>
        public static bool canUseAffectSkill(IBattleable actioner,List<IBattleable> targets,IActiveSkill skill){
            UnityEngine.Debug.Log("start judge " + skill.getName());

            if (!isAffectSkill(skill))
                throw new ArgumentException("gave skill isn't an affectSkill");

            //スキル使用者のMPが足りるか
            if (actioner.getMp() < skill.getCost())
                return false;

            //スキル使用対象はちゃんと存在するか
            if (targets.Count <= 0)
                return false;
            
            //スキル使用対象まで射程が届くか
            int actionerPos = (int)BattleManager.getInstance().searchCharacter(actioner);
            int targetPos = (int)BattleManager.getInstance().searchCharacter(targets[0]);
            int distance = (actionerPos < targetPos) ? targetPos - actionerPos : actionerPos - targetPos;
            if(distance > searchRange(skill, actioner)) {
                return false;
            }

            UnityEngine.Debug.Log("end judge " + skill.getName());

            //以上が満たされればtrue
            return true;
        }
	}
}

