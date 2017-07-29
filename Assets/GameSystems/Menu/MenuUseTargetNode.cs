using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Character;

namespace Menus {
    public class MenuUseTargetNode : MonoBehaviour {
        private IPlayable character;
        private MenuUseWindow window;

        public Text nameText;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void setState(IPlayable character,MenuUseWindow window) {
            this.character = character;
            this.window = window;
        }

        public void chosen() {
            window.targetChosen(character);
        }
    }
}