using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;

namespace BattleSystem{
	public class PassiveSkillNode : MonoBehaviour {
		private PassiveSkill skill;
		public Text text;
		private PlayerBattleTaskManager manager;

		public void setState(PassiveSkill skill,PlayerBattleTaskManager manager){
			this.skill = skill;
			text.text = skill.getName ();
			this.manager = manager;
		}
			
		public void chosen(){
			manager.passiveChose (skill);
		}
	}
}
