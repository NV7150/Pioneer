using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;
using Character;

namespace Menus {
    public class MenuUseWindow : MonoBehaviour {
        MenuItemView view;
        Party party;
        IItem item;

        public GameObject content;

        GameObject useTargetNodePrefab;

        // Use this for initialization
        void Awake() {
            useTargetNodePrefab = (GameObject)Resources.Load("Prefabs/MenuUseTargetNode");

        }

        // Update is called once per frame
        void Update() {

        }

        public void setState(MenuItemView view ,IItem item,Party party){
            this.view = view;
            this.item = item;
            this.party = party;
            inputTargets();
        }

        private void inputTargets(){
            foreach(IPlayable character in party.getParty()){
                MenuUseTargetNode useTargetNode = Instantiate(useTargetNodePrefab).GetComponent<MenuUseTargetNode>();
                useTargetNode.setState(character,this);
                useTargetNode.transform.SetParent(content.transform);
            }
        }

        public void targetChosen(IPlayable target){
            view.useTargetChose(target,this);
        }

		public void cancelChose() {
			CanvasGetter.getCanvas().GetComponent<CanvasGroup>().interactable = true;
            CanvasGetter.getCanvas().GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.root.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Destroy(gameObject);
        }
    }
}