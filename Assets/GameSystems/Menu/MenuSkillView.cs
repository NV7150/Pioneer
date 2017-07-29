using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;

namespace Menus {
    public class MenuSkillView : MonoBehaviour {
        public Text nameText;
        public Text descriptionText;
        public Text costText;
        public Text flavorText;

        private Menu menu;

        public void printSkill(ISkill skill,Menu menu){
            nameText.text = skill.getName();
            descriptionText.text = skill.getDescription();
            costText.text = "" + skill.getCost();
            flavorText.text = skill.getFlavorText();

            this.menu = menu;
        }

        public void deleteChosen(){
            Destroy(gameObject);
        }
    }
}
