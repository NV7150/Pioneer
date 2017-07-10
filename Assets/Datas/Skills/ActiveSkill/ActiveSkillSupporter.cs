using System;

using Character;
using Extent = Skill.ActiveSkillParameters.Extent;
using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Skill {
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
		/// 対象が必要なスキルかを判定します
		/// </summary>
		/// <returns><c>true</c>, ターゲットが必要 <c>false</c> 必要なし</returns>
		/// <param name="skill">判定したいスキル</param>
		public static bool needsTarget(IActiveSkill skill){
			ActiveSkillType type = skill.getActiveSkillType ();
			bool needsTarget = (
				type == ActiveSkillType.ATTACK ||
				type == ActiveSkillType.BUF ||
				type == ActiveSkillType.DEBUF ||
				type == ActiveSkillType.HEAL
			);
			return needsTarget;
		}
	}
}

