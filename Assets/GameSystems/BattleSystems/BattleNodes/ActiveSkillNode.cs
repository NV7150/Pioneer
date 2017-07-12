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
		private IActiveSkill skill;
		//元のPlayerBattleTaskManagerです
		private PlayerBattleTaskManager manager;

		//  
		public void setState(PlayerBattleTaskManager controller,IActiveSkill skill){
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
