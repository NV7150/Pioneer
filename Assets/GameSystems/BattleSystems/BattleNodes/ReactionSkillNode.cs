using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;

namespace BattleSystem{
	public class ReactionSkillNode : MonoBehaviour {
		//担当するスキルを表します
		private ReactionSkill skill;
		//アタッチされているGameObjectの子オブジェクトのTextObjectを表します
		public Text text;
		//元のPlayerBattleTaskManagerです
		private PlayerBattleTaskManager manager;

		//ステートを設定します
		public void setState(ReactionSkill skill,PlayerBattleTaskManager manager){
			this.skill = skill;
			text.text = skill.getName ();
			this.manager = manager;
		}

		//選ばれた時の処理
		public void chosen(){
			manager.reactionChose (skill);
		}
	}
}
