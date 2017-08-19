using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;
using SelectView;
using System;

namespace Menus {
    public class MenuSkillNode : MonoBehaviour ,INode<ISkill>{
		/// <summary> 担当するスキル </summary>
		private ISkill skill;

        /// <summary> 名前を表示するテキスト </summary>
        public Text nameText;

        /// <summary>
        /// 初期設定
        /// </summary>
        /// <param name="skill">担当するスキル</param>
        public void setSkill(ISkill skill) {
            this.skill = skill;

            nameText.text = skill.getName();
        }

        public ISkill getElement() {
            return skill;
        }
    }
}
