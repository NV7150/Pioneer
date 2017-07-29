using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;
using Character;
using System;

namespace Menus{
    public class MenuItemNode : MonoBehaviour {
        IItem item;
        ItemStack stack;
        Menu menu;

        bool isStack = false;

        public Text nameText;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void setState(IItem item, Menu menu) {
            this.item = item;
            this.menu = menu;
            nameText.text = item.getName();

            isStack = false;
        }

        public void setState(ItemStack stack ,Menu menu){
            this.stack = stack;
            this.menu = menu;
            nameText.text = stack.getItem().getName();

            isStack = true;
        }

        public void chosen(){
            if (isStack) {
                menu.itemChosen(stack);
            } else {
                menu.itemChosen(item);
            }
        }
    }
}
