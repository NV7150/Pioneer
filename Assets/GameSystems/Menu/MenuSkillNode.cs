using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;
using System;

namespace Menus {
    public class MenuSkillNode : MonoBehaviour {
		/// <summary> 担当するスキル </summary>
		private ISkill skill;
		/// <summary> 元となるメニュー </summary>
		private Menu menu;


        /// <summary> 名前を表示するテキスト </summary>
        public Text nameText;

        /// <summary>
        /// 初期設定
        /// </summary>
        /// <param name="skill">担当するスキル</param>
        /// <param name="menu">Menu.</param>
        public void setState(ISkill skill, Menu menu) {
            this.skill = skill;
            this.menu = menu;

            nameText.text = skill.getName();
        }

        /// <summary>
        /// 選択された時の処理
        /// </summary>
        public void chosen(){
            menu.skillChose(skill);
        }
    }
}
