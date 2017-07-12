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

        /// <summary>
        /// 初期設定をします
        /// </summary>
        /// <param name="manager"> 情報を取得するPlayerBattleTaskManager </param>
        /// <param name="skill"> 担当するスキル </param>
        public void setState(PlayerBattleTaskManager manager,IActiveSkill skill){
			this.manager = manager;   
			this.skill = skill; 
			textObject.text = skill.getName ();
		}

		//選択された時の動作です
		public void chosen(){
			manager.skillChose (skill);
		}
	}
}
