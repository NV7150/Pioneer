using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;

namespace Menus{
    public class MenuCharacterNode : MonoBehaviour {
        private IPlayable character;
        private Menu menu;

        public Text nameText;

        public void setCharacter(IPlayable character, Menu menu) {
            this.character = character;
            this.menu = menu;

            nameText.text = character.getName();
        }

        public void chosen() {
            menu.characterChose(character);
        }
    }
}
