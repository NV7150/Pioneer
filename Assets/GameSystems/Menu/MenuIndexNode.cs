using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MenuContent = Menus.MenuParameters.MenuContents;

namespace Menus {
    public class MenuIndexNode : MonoBehaviour {
        MenuContent content;
        Menu menu;

        public Text contentText;

        // Use this for initialization
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {

        }

        public void setState(MenuContent content,Menu menu){
            this.content = content;
            this.menu = menu;

            //かり
            contentText.text = "" + content;
        }

        public void chosen(){
            menu.indexChosen(content);
        }
    }
}
