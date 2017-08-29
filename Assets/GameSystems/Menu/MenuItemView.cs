using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Item;
using Character;
using System;

namespace Menus {
    public class MenuItemView : MonoBehaviour {
		/// <summary> 担当するアイテム </summary>
		private IItem item;
		/// <summary> プレイヤーが所属するパーティ </summary>
		private Party party;

		/// <summary> 名前を表示するテキスト </summary>
		public Text nameText;
		/// <summary> 説明文を表示するテキスト </summary>
		public Text descritionText;
		/// <summary> アイテム価格を表示するテキスト </summary>
		public Text valueText;
        /// <summary> アイテム品質を表示するテキスト </summary>
        public Text qualityText;
		/// <summary> アイテム重量を表示するテキスト </summary>
		public Text massText;
		/// <summary> アイテム数量を表示するテキスト </summary>
		public Text numberText;
		/// <summary> アイテムのフレーバーテキストを表示するテキスト </summary>
		public Text flavorText;

        /// <summary> 使うウィンドウのプレファブ </summary>
        private GameObject menuUseWindowPrefab;

        /// <summary> 元のメニュー </summary>
        private Menu menu;

        /// <summary> 担当するアイテムがスタックを持つかのフラグ </summary>
        private bool hasStack;

        // Use this for initialization
        void Awake() {
            menuUseWindowPrefab = (GameObject)Resources.Load("Prefabs/MenuUseWindow");
        }

        /// <summary>
        /// アイテムを設定します
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <param name="targets">PCのパーティ</param>
        /// <param name="menu">元のパーティ</param>
        public void setItem(IItem item,Party targets,Menu menu){
            this.item = item;

            nameText.text = item.getName();
            descritionText.text = item.getDescription();
            valueText.text = "価格 " + item.getItemValue();
            massText.text = "重さ " + item.getMass();
            flavorText.text = item.getFlavorText();

            this.hasStack = item is ItemStack;

			if (hasStack) {
                var stack = (ItemStack)item;
				numberText.gameObject.SetActive(true);
				numberText.text = "" + stack.getNumberOfStack();
            }else{
				numberText.gameObject.SetActive(false);
            }


            if(ItemHelper.isEquipment(item)){
                string text = "品質 " + (int)ItemHelper.searchQuality(item);
                qualityText.text = text;
			}
			this.party = targets;

            foreach(ICharacter chara in party.getParty()){
                Debug.Log(chara.getName());
            }


            this.menu = menu;
        }

        /// <summary>
        /// 使うが選ばれた時の処理
        /// </summary>
        public void useChose(){
            Vector3 windowPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            MenuUseWindow useWindow = Instantiate(menuUseWindowPrefab,windowPos,new Quaternion(0,0,0,0)).GetComponent<MenuUseWindow>();
            useWindow.transform.SetParent(CanvasGetter.getCanvasElement().transform);
            useWindow.setState(this,item,party);
            menu.setIsWindowInputing(true);
        }

		/// <summary>
		/// 使う対象が決定した時の処理
		/// </summary>
		/// <param name="target">使う対象のリスト</param>
		/// <param name="window">操作された使うウィンドウ</param>
		public void useTargetChose(IPlayable target, MenuUseWindow window) {
			item.use(target);

            if(hasStack){
                var stack = (ItemStack)item;
                if(!stack.hasStack()){
                    window.cancelChose();
                }
            }else{
                window.cancelChose();
            }
            Debug.Log("into useChosen " + item.getName());
        }

        public void finishUsing(){
            menu.setIsWindowInputing(false);
        }
    }
}