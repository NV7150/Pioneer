using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;

namespace BattleSystem{
	public class ReactionSkillNode : MonoBehaviour {
		/// <summary>
        /// 担当するReacitonSkill
        /// </summary>
		private ReactionSkill skill;
		/// <summary>
		/// アタッチされているGameObjectの子オブジェクトのTextオブジェクト
		/// </summary>
		public Text text;
		/// <summary>
        /// 元のBattleTaskManger
        /// </summary>
		private PlayerBattleTaskManager manager;

		/// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="skill">担当するスキル</param>
        /// <param name="manager">元のBattleTaskManager</param>
		public void setState(ReactionSkill skill,PlayerBattleTaskManager manager){
			this.skill = skill;
			text.text = skill.getName ();
			this.manager = manager;
		}

		/// <summary>
        /// 選ばれた時の処理
        /// </summary>
		public void chosen(){
			manager.reactionChose (skill);
		}
	}
}
