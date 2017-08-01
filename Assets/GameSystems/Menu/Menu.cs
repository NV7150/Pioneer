using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using Item;
using Skill;

using MenuContents = Menus.MenuParameters.MenuContents;

namespace Menus {
    public class Menu : MonoBehaviour {
        /// <summary> スクロールビューのコンテント </summary>
        public GameObject content;

        //各ノード・ビューのプレファブ
        private GameObject menuIndexNodePrefab;
        private GameObject menuItemNodePrefab;
        private GameObject menuSkillNodePrefab;
        private GameObject menuItemViewPrefab;
        private GameObject menuSkillViewPrefab;
		private GameObject useWindowPrefab;
        private GameObject menuCharacterNodePrefab;
        private GameObject menuCharacterViewPrefab;

        /// <summary> プレイヤーが所属するパーティ </summary>
        Party party;
        /// <summary> PC </summary>
        private Hero player;

		/// <summary> アクティブなアイテムビュー </summary>
		MenuItemView itemView;
		/// <summary> アクティブなスキルビュー </summary>
		MenuSkillView skillView;
		/// <summary> アクティブなステートビュー </summary>
		MenuCharacterStateView stateView;

        // Use this for initialization
        void Awake() {
            menuIndexNodePrefab = (GameObject)Resources.Load("Prefabs/MenuIndexNode");
            menuItemNodePrefab = (GameObject)Resources.Load("Prefabs/MenuItemNode");
            menuSkillNodePrefab = (GameObject)Resources.Load("Prefabs/MenuSkillNode");
            menuItemViewPrefab = (GameObject)Resources.Load("Prefabs/MenuItemView");
            menuSkillViewPrefab = (GameObject)Resources.Load("Prefabs/MenuSkillView");
            menuCharacterNodePrefab = (GameObject)Resources.Load("Prefabs/MenuCharacterNode");
            menuCharacterViewPrefab = (GameObject)Resources.Load("Prefabs/MenuCharacterStateView");
            useWindowPrefab = (GameObject)Resources.Load("Prefabs/UseWindow");
        }

        // Update is called once per frame
        void Update() {

        }

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="party">playerが所属するパーティ</param>
        public void setState(Hero player,Party party){
            this.player = player;
            this.party = party;
            inputIndex();
        }

        /// <summary>
        /// インデックスをスクロールビューに表示させます
        /// </summary>
        private void inputIndex(){
            var contents = Enum.GetValues(typeof(MenuContents));
            foreach(MenuContents menuContent in contents){
                MenuIndexNode menuIndexNode = Instantiate(menuIndexNodePrefab).GetComponent<MenuIndexNode>();
                menuIndexNode.setState(menuContent,this);
                menuIndexNode.transform.SetParent(content.transform);
            }
        }

		/// <summary>
		/// インデックスが選ばれた時の処理
		/// </summary>
		public void indexChosen(MenuContents menuContent){
            detachContents();

            switch(menuContent){
                case MenuContents.STATUS:
                    inputCharacters();
                    break;
                case MenuContents.ITEM:
                    inputItems();
                    break;

                case MenuContents.SKILL:
                    inputSkills();
                    break;

                default:
                    throw new NotSupportedException("unkonwn content " + menuContent);
            }
        }

		/// <summary>
		/// パーティのキャラクターをスクロールビューに表示させます
		/// </summary>
		public void inputCharacters(){
            foreach(IPlayable character in party.getParty()){
                GameObject characterNodeObject = Instantiate(menuCharacterNodePrefab);
                MenuCharacterNode characterNode = characterNodeObject.GetComponent<MenuCharacterNode>();
                characterNode.setCharacter(character,this);
                characterNode.transform.SetParent(content.transform);
            }
        }

		/// <summary>
		/// キャラクターが選ばれた時の処理
		/// </summary>
		public void characterChose(IPlayable character){
            if (stateView == null) {
                GameObject viewObject = Instantiate(menuCharacterViewPrefab, new Vector3(360, 384, 0), new Quaternion(0, 0, 0, 0));
                stateView = viewObject.GetComponent<MenuCharacterStateView>();
                stateView.transform.SetParent(CanvasGetter.getCanvas().transform);
            }
            stateView.setCharacter(character);
        }

		/// <summary>
		/// インベントリ内のアイテムをスクロールビューに表示させます
		/// </summary>
		public void inputItems() {
			var inventry = player.getInventry();
            var items = inventry.getItems();
            foreach(IItem item in items){
                MenuItemNode itemNode = Instantiate(menuItemNodePrefab).GetComponent<MenuItemNode>();
                if (item.getCanStack()) {
                    itemNode.setState(inventry.getStack(item), this);
				} else {
					itemNode.setState(item, this);
                }
                itemNode.transform.SetParent(content.transform);
            }
        }

		/// <summary>
        /// アイテムが選択された時の処理
        /// </summary>
        /// <param name="item">選択されたアイテム</param>
		public void itemChosen(IItem item) {
			inputItemView();

            itemView.setItem(item,party,this);
        }

		/// <summary>
		/// アイテムが選択された時の処理
		/// </summary>
		public void itemChose(ItemStack stack) {
            inputItemView();

            itemView.setItem(stack, party, this);
        }

        /// <summary>
        /// アイテムビューにアイテムを表示させます
        /// </summary>
        private void inputItemView(){
			if (itemView == null) {
                itemView = Instantiate(menuItemViewPrefab,new Vector3(360,384,0),new Quaternion(0,0,0,0)).GetComponent<MenuItemView>();
				itemView.transform.SetParent(CanvasGetter.getCanvas().transform);
			}
        }

		/// <summary>
		/// スキルをスクロールビューに表示させます
		/// </summary>
		public void inputSkills(){
            //修正可
            List<IActiveSkill> askills = new List<IActiveSkill>();
            askills.AddRange(player.getActiveSkills());
            List<ReactionSkill> rskills = new List<ReactionSkill>();
            rskills.AddRange(player.getReactionSKills());

            foreach(ISkill skill in askills){
                MenuSkillNode skillNode = Instantiate(menuSkillNodePrefab).GetComponent<MenuSkillNode>();
                skillNode.setState(skill,this);
                skillNode.transform.SetParent(content.transform);
            }
			foreach (ISkill skill in rskills) {
				MenuSkillNode skillNode = Instantiate(menuSkillNodePrefab).GetComponent<MenuSkillNode>();
				skillNode.setState(skill, this);
				skillNode.transform.SetParent(content.transform);
			}
        }

        /// <summary>
        /// スキル選択時の処理
        /// </summary>
        /// <param name="skill">選択されたスキル</param>
        public void skillChose(ISkill skill){
            if(skillView == null){
                skillView = Instantiate(menuSkillViewPrefab,new Vector3(360, 384, 0), new Quaternion(0, 0, 0, 0)).GetComponent<MenuSkillView>();
                skillView.transform.SetParent(CanvasGetter.getCanvas().transform);
            }
            skillView.printSkill(skill,this);
        }

		/// <summary>
		/// contentsオブジェクトの子ノードを削除します
		/// </summary>
		private void detachContents() {
			Transform children = content.GetComponentInChildren<Transform>();
			foreach (Transform child in children) {
				Destroy(child.gameObject);
			}
		}

        /// <summary>
        /// 戻るが選択された時の処理
        /// </summary>
        public void backChose(){
            detachContents();

            skillView = null;
            itemView = null;
            stateView = null;

            inputIndex();
        }

        /// <summary>
        /// 終了が選択された時の処理
        /// </summary>
        public void finishChose(){
            if(skillView != null )
                Destroy(skillView.gameObject);

            if (itemView != null)
                Destroy(itemView.gameObject);

            Destroy(gameObject);
        }
    }
}
