using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;
using Character;
using System;

namespace Menus {
    public class MenuItemView : MonoBehaviour {
        private IItem item;
        private ItemStack stack;
        private Party party;

        public Text nameText;
        public Text descritionText;
        public Text valueText;
        public Text massText;
        public Text numberText;
        public Text flavorText;

        private GameObject menuUseWindowPrefab;

        private Menu menu;

        private bool hasStack;


        private Canvas frontCanvas;

        // Use this for initialization
        void Awake() {
			frontCanvas = GameObject.Find("FrontCanvas").GetComponent<Canvas>();
            menuUseWindowPrefab = (GameObject)Resources.Load("Prefabs/MenuUseWindow");
        }

        // Update is called once per frame
        void Update() {

        }

        public void setItem(IItem item,Party targets,Menu menu){
            numberText.gameObject.SetActive(false);
            this.item = item;

            nameText.text = item.getName();
            descritionText.text = item.getDescription();
            valueText.text = "" + item.getItemValue();
            massText.text = "" + item.getMass();
            flavorText.text = item.getFlavorText();

            this.party = targets;

            this.hasStack = false;

            this.menu = menu;
        }

        public void setItem(ItemStack stack,Party targets,Menu menu){
			numberText.gameObject.SetActive(true);
            this.stack = stack;
            item = stack.getItem();

			nameText.text = item.getName();
			descritionText.text = item.getDescription();
			valueText.text = "" + item.getItemValue();
			massText.text = "" + item.getMass();
            numberText.text = "" + stack.getNumberOfStack();
			flavorText.text = item.getFlavorText();

            this.party = targets;

            this.hasStack = true;

            this.menu = menu;
        }

        public void useChose(){
            Vector3 windowPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            MenuUseWindow useWindow = Instantiate(menuUseWindowPrefab,windowPos,new Quaternion(0,0,0,0)).GetComponent<MenuUseWindow>();
            useWindow.transform.SetParent(frontCanvas.transform);
            frontCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
            CanvasGetter.getCanvas().GetComponent<CanvasGroup>().interactable = false;
            CanvasGetter.getCanvas().GetComponent<CanvasGroup>().blocksRaycasts = false;
            useWindow.setState(this,item,party);
        }

        public void useTargetChose(IPlayable target,MenuUseWindow window){
            if(hasStack){
                if (!stack.take()) {
                    window.cancelChose();
                }
            }else{
                window.cancelChose();
            }

            item.use(target);
        }

        public void deleteChosen(){
            Destroy(gameObject);
        }
    }
}