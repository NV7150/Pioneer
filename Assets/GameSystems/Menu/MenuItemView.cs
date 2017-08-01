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
		/// <summary> 担当するアイテムスタック </summary>
		private ItemStack stack;
		/// <summary> プレイヤーが所属するパーティ </summary>
		private Party party;

		/// <summary> 名前を表示するテキスト </summary>
		public Text nameText;
		/// <summary> 説明文を表示するテキスト </summary>
		public Text descritionText;
		/// <summary> アイテム価格を表示するテキスト </summary>
		public Text valueText;
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

        /// <summary> ウィンドウを表示するカンヴァス </summary>
        private Canvas frontCanvas;

        // Use this for initialization
        void Awake() {
			frontCanvas = GameObject.Find("FrontCanvas").GetComponent<Canvas>();
            menuUseWindowPrefab = (GameObject)Resources.Load("Prefabs/MenuUseWindow");
        }

        /// <summary>
        /// アイテムを設定します
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <param name="targets">PCのパーティ</param>
        /// <param name="menu">元のパーティ</param>
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

		/// <summary>
		/// アイテムを設定します
		/// </summary>
		/// <param name="stack">アイテムスタック</param>
		/// <param name="targets">PCのパーティ</param>
		/// <param name="menu">元のパーティ</param>
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

        /// <summary>
        /// 使うが選ばれた時の処理
        /// </summary>
        public void useChose(){
            Vector3 windowPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            MenuUseWindow useWindow = Instantiate(menuUseWindowPrefab,windowPos,new Quaternion(0,0,0,0)).GetComponent<MenuUseWindow>();
            useWindow.transform.SetParent(frontCanvas.transform);
            frontCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
            CanvasGetter.getCanvas().GetComponent<CanvasGroup>().interactable = false;
            CanvasGetter.getCanvas().GetComponent<CanvasGroup>().blocksRaycasts = false;
            useWindow.setState(this,item,party);
        }

        /// <summary>
        /// 使う対象が決定した時の処理
        /// </summary>
        /// <param name="target">使う対象のリスト</param>
        /// <param name="window">操作された使うウィンドウ</param>
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

        /// <summary>
        /// 削除が選ばれた時の処理
        /// </summary>
        public void deleteChosen(){
            Destroy(gameObject);
        }
    }
}