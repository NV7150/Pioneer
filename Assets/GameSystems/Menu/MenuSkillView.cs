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

        /// <summary>元となるメニュー</summary>
        private Menu menu;

        /// <summary>
        /// 対象を表示します
        /// </summary>
        /// <param name="skill">情報を表示したいスキル</param>
        /// <param name="menu">元となるメニュー</param>
        public void printSkill(ISkill skill,Menu menu){
            nameText.text = skill.getName();
            descriptionText.text = skill.getDescription();
            costText.text = "" + skill.getCost();
            flavorText.text = skill.getFlavorText();

            this.menu = menu;
        }

        /// <summary> 削除が選ばれた時の処理 </summary>
        public void deleteChosen(){
            //Destroy(gameObject);
        }
    }
}
