using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;

namespace BattleSystem{
	public class ActiveSkillNode : MonoBehaviour {
		//アタッチされているゲームオブジェクトのテキストオブジェクトです
		public Text textObject;
		//担当しているActiveSkillです
		private ActiveSkill skill;
		//元のPlayerBattleTaskManagerです
		private PlayerBattleTaskManager manager;

		//ステータスを設定します
		public void setState(PlayerBattleTaskManager controller,ActiveSkill skill){
			this.manager = controller;
			this.skill = skill;
			textObject.text = skill.getName ();
		}

		//選択された時の動作です
		public void chosen(){
			manager.skillChose (skill);
		}
	}
}
