using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Skill;
using System;

namespace Menus {
    public class MenuSkillNode : MonoBehaviour {
        private ISkill skill;
        private Menu menu;

        public Text nameText;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void setState(ISkill skill, Menu menu) {
            this.skill = skill;
            this.menu = menu;

            nameText.text = skill.getName();
        }

        public void chosen(){
            menu.skillChosen(skill);
        }
    }
}
