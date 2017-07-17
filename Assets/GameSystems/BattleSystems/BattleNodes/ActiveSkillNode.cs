using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;
 
namespace BattleSystem{
	public class ActiveSkillNode : MonoBehaviour {
		/// <summary> アタッチされているGameObjectのTextオブジェクト </summary>
		public Text textObject;
		/// <summary> 担当するActiveSkill </summary>
		private IActiveSkill skill;
        /// <summary> 元のタスクマネージャ </summary>
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

		/// <summary>
        /// 選択された時の動作
        /// </summary>
		public void chosen(){
			manager.skillChose (skill);
		}
	}
}
