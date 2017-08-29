using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;
using Character;

namespace Menus {
    public class MenuUseWindow : MonoBehaviour {
        /// <summary>元となるメニュー</summary>
        MenuItemView view;
        /// <summary> PCが所属するパーティ </summary>
        Party party;
        /// <summary> 使用するアイテム </summary>
        IItem item;

        /// <summary> スクロールビューのコンテント </summary>
        public GameObject content;

        /// <summary> UseTargetNodeのプレファブ </summary>
        GameObject useTargetNodePrefab;

        // Use this for initialization
        void Awake() {
            useTargetNodePrefab = (GameObject)Resources.Load("Prefabs/MenuUseTargetNode");

        }

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="view">元となるMenuItemView</param>
        /// <param name="item">使用するアイテム</param>
        /// <param name="party">PCが所属するパーティ</param>
        public void setState(MenuItemView view ,IItem item,Party party){
            this.view = view;
            this.item = item;
            this.party = party;
            inputTargets();
        }

        /// <summary>
        /// 使用対象を表示します
        /// </summary>
        private void inputTargets(){
            foreach(IPlayable character in party.getParty()){
                MenuUseTargetNode useTargetNode = Instantiate(useTargetNodePrefab).GetComponent<MenuUseTargetNode>();
                useTargetNode.setState(character,this);
                useTargetNode.transform.SetParent(content.transform);
            }
        }

        /// <summary>
        /// 使用対象が選ばれたときの処理
        /// </summary>
        /// <param name="target">使用対象</param>
        public void targetChosen(IPlayable target){
            view.useTargetChose(target,this);
        }

        /// <summary>
        /// キャンセルが選ばれた時の処理
        /// </summary>
		public void cancelChose() {
            view.finishUsing();
            Destroy(gameObject);
        }
    }
}