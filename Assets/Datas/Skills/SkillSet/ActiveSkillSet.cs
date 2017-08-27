using System;
using System.Collections;
using System.Collections.Generic;

using Skill;
using Parameter;
using MasterData;
using Character;
using AI;

namespace Skill {
	public class ActiveSkillSet {
		/// <summary> スキルセットに設定されているスキルのDictionary </summary>
		private Dictionary<ActiveSkillCategory,IActiveSkill> skillSet = new Dictionary<ActiveSkillCategory, IActiveSkill> ();

		/// <summary> スキルセットのID </summary>
		private readonly int ID;

		/// <summary> スキルセットの中で最大の射程 </summary>
        private readonly int MAX_RANGE;
		/// <summary> スキルセット名 </summary>
		private readonly string NAME;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="builder"> スキルセットのビルダー </param>
        /// <param name="user"> スキルセットを使うIBattleableキャラクター </param>
		public ActiveSkillSet (ActiveSkillSetBuilder builder,IBattleable user) {
			this.ID = builder.getId ();
			this.NAME = builder.getName ();

			skillSet [ActiveSkillCategory.NORMAL] = builder.getNormalSkill ();
			skillSet [ActiveSkillCategory.CAUTION] = builder.getCautionSkill ();
			skillSet [ActiveSkillCategory.DANGER] = builder.getDangerSkill ();
			skillSet [ActiveSkillCategory.POWER] = builder.getPowerSkill ();
			skillSet [ActiveSkillCategory.FULL_POWER] = builder.getFullPowerSkill ();
			skillSet [ActiveSkillCategory.SUPPORT] = builder.getSupportSkill ();
			skillSet [ActiveSkillCategory.HEAL] = builder.getHealSkill ();
			skillSet [ActiveSkillCategory.MOVE] = builder.getMoveSkill ();

            MAX_RANGE = calculateMaxRange (user);
		}

		/// <summary>
        /// 最大射程を算出します
        /// </summary>
        /// <returns> 最大射程 </returns>
        /// <param name="user"> スキルを使用するIBattleableオブジェクト </param>
		private int calculateMaxRange(IBattleable user){
			int maxRange = 0;
			var keys = skillSet.Keys;
			foreach(ActiveSkillCategory category in keys){
				if (ActiveSkillSupporter.isAffectSkill (skillSet [category])) {
					int skillRange = ActiveSkillSupporter.searchRange (skillSet[category],user);
					if (skillRange > maxRange)
						maxRange = skillRange;
				}
			}
			return maxRange;
		}

		/// <summary>
        /// スキルセットのIDを取得します
        /// </summary>
        /// <returns>ID</returns>
		public int getId(){
			return ID;
		}

		/// <summary>
        /// スキルセット名を取得します
        /// </summary>
        /// <returns>スキルセット名</returns>
		public string getName(){
			return NAME;
		}

		/// <summary>
        /// スキルセットの最大射程を取得します
        /// </summary>
        /// <returns>最大射程</returns>
		public int getMaxRange(){
            return MAX_RANGE;
		}

		/// <summary>
		/// 指定したカテゴリからスキルを取得します
		/// </summary>
		/// <returns>指定したスキル</returns>
		/// <param name="category">取得したいスキルのカテゴリ</param>
		public IActiveSkill getSkillFromSkillCategory(ActiveSkillCategory category){
			return skillSet[category];
		}
	}
}

