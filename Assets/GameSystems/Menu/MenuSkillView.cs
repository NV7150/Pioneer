using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;

namespace Menus {
    public class MenuSkillView : MonoBehaviour {
		/// <summary> 名前を表示するテキスト </summary>
		public Text nameText;
		/// <summary> 説明文を表示するテキスト </summary>
		public Text descriptionText;
		/// <summary> コストを表示するテキスト </summary>
		public Text costText;
		/// <summary> フレーバーテキストを表示するテキスト </summary>
		public Text flavorText;

        /// <summary>
        /// 対象を表示します
        /// </summary>
        /// <param name="skill">情報を表示したいスキル</param>
        public void printSkill(ISkill skill){
            nameText.text = skill.getName();
            descriptionText.text = skill.getDescription();
            costText.text = "" + skill.getCost();
            flavorText.text = skill.getFlavorText();

        }
    }
}
