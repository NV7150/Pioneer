using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;

namespace BattleSystem{
	public class ActiveSkillNode : MonoBehaviour {
		public Text textObject;
		private ActiveSkill skill;
		private PlayerBattleTaskManager controller;

		public void setState(PlayerBattleTaskManager controller,ActiveSkill skill){
			this.controller = controller;
			this.skill = skill;
			textObject.text = skill.getName ();
		}

		public void chosen(){
			controller.skillChose (skill);
		}
	}
}
