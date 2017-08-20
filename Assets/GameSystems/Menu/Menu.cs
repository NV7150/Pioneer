﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using Item;
using Skill;
using SelectView;

using MenuContents = Menus.MenuParameters.MenuContents;
//using static Menus.MenuParameters.MenuContents;

namespace Menus {
    public class Menu : MonoBehaviour {
        /// <summary>
        /// セレクトビューのコンテナ
        /// </summary>
        private SelectViewContainer selectviewContainer;

        private SelectView<MenuIndexNode,MenuContents> indexSelectView;
        private SelectView<MenuItemNode, IItem> itemSelectView;
        private SelectView<MenuSkillNode, ISkill> skillSelectView;
        private SelectView<MenuCharacterNode, IPlayable> characterSelectView;

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

        MenuContents currentContent = MenuContents.INDEX;

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

            selectviewContainer = Instantiate((GameObject)Resources.Load("Prefabs/SelectView")).GetComponent<SelectViewContainer>();
            Debug.Log("selectview instantiated");
            selectviewContainer.transform.position = transform.position;
            selectviewContainer.GetComponent<RectTransform>().sizeDelta -= new Vector2(0,60);
            selectviewContainer.transform.position -= new Vector3(50, 30, 0);
            selectviewContainer.transform.SetParent(transform);
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
                int axis = getAxis();

                if (axis != 0) {
                    moveCursor(axis);
                }
            }

            if(Input.GetKeyDown(KeyCode.Return) && currentContent == MenuContents.INDEX){
                indexChosen(indexSelectView.getElement());
            }
		}

		private int getAxis() {
			int axis = 0;
			float rawAxis = Input.GetAxisRaw("Vertical");
			if (rawAxis > 0) {
				axis = -1;
			} else if (rawAxis < 0) {
				axis = 1;
			}
            return axis;
        }

        private void moveCursor(int axis){
            switch(currentContent){
                case MenuContents.INDEX:
                    try { 
                        indexSelectView.moveTo(indexSelectView.getIndex() + axis); 
                    }catch{
                        Debug.Log("there are no indexes");
                    }
                    break;

                case MenuContents.ITEM:
                    try {
						var item = itemSelectView.moveTo(itemSelectView.getIndex() + axis);
						inputItemView(item);
                    }catch {
                        Debug.Log("there are no items");
                    }
                    break;

                case MenuContents.SKILL:
					try {
						var skill = skillSelectView.moveTo(skillSelectView.getIndex() + axis);
						inputSkillView(skill);
					}catch{
                        Debug.Log("there are no skills");
                    }

                    break;

                case MenuContents.STATUS:
                    try {
                        var character = characterSelectView.moveTo(characterSelectView.getIndex() + axis);
                        inputCharacterStateView(character);
                    }catch{
                        Debug.Log("there are no characters in this party");
                    }
                    break;
            }
        }

        /// <summary>
        /// 初期設定を行います
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="party">playerが所属するパーティ</param>
        public void setState(Hero player,Party party){
            Debug.Log("into index");

            this.player = player;
            this.party = party;
            inputIndex();
        }

		/// <summary>
		/// インデックスをスクロールビューに表示させます
		/// </summary>
		private void inputIndex() {
            currentContent = MenuContents.INDEX;
            deleteCursors();

			selectviewContainer.detach();

            var contents = Enum.GetValues(typeof(MenuContents));
            List<MenuIndexNode> contentsList = new List<MenuIndexNode>();
            foreach(MenuContents menuContent in contents){
                if (menuContent != MenuContents.INDEX) {
                    var indexNode = Instantiate(menuIndexNodePrefab).GetComponent<MenuIndexNode>();
                    indexNode.setContent(menuContent);
                    contentsList.Add(indexNode);
                }
            }

            indexSelectView = selectviewContainer.creatSelectView<MenuIndexNode, MenuContents>(contentsList); 
        }

        /// <summary>
        /// インデックスが選ばれた時の処理
        /// </summary>
        public void indexChosen(MenuContents menuContent){
            selectviewContainer.detach();

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

            currentContent = menuContent;
        }

		/// <summary>
		/// パーティのキャラクターをスクロールビューに表示させます
		/// </summary>
		public void inputCharacters() {
            selectviewContainer.detach();
            deleteCursors();
            
            List<MenuCharacterNode> characterNodes = new List<MenuCharacterNode>();
            foreach(IPlayable character in party.getParty()){
                GameObject characterNodeObject = Instantiate(menuCharacterNodePrefab);
                MenuCharacterNode characterNode = characterNodeObject.GetComponent<MenuCharacterNode>();
                characterNode.setCharacter(character);
                characterNodes.Add(characterNode);
            }
            characterSelectView = selectviewContainer.creatSelectView<MenuCharacterNode, IPlayable>(characterNodes);

            try {
                inputCharacterStateView(characterSelectView.getElement());
            }catch{
                Debug.Log("there are no characters");
            }
        }

        /// <summary>
        /// キャラクターが選ばれた時の処理
        /// </summary>
        public void inputCharacterStateView(IPlayable character){
            if (stateView == null) {
                GameObject viewObject = Instantiate(menuCharacterViewPrefab, new Vector3(312, 384, 0), new Quaternion(0, 0, 0, 0));
                stateView = viewObject.GetComponent<MenuCharacterStateView>();
                stateView.transform.SetParent(CanvasGetter.getCanvas().transform);
            }
            stateView.setCharacter(character);
        }

        /// <summary>
        /// インベントリ内のアイテムをスクロールビューに表示させます
        /// </summary>
        public void inputItems() {
			selectviewContainer.detach();
			deleteCursors();

            var inventory = player.getInventory();
            var items = inventory.getItems();
            var nodes = new List<MenuItemNode>();
            foreach (IItem item in items) {
                var itemNode = Instantiate(menuItemNodePrefab).GetComponent<MenuItemNode>();
                if (item.getCanStack()) {
                    itemNode.setItem(inventory.getStack(item));
                } else {
                    itemNode.setItem(item);
                }
                nodes.Add(itemNode);
            }

            itemSelectView = selectviewContainer.creatSelectView<MenuItemNode, IItem>(nodes);

            try { 
                inputItemView(itemSelectView.getElement()); 
            }catch{
                Debug.Log("there are no items");
            }
        }

        /// <summary>
        /// アイテムビューにアイテムを表示させます
        /// </summary>
        private void inputItemView(IItem item){
            if (itemView == null) {
                itemView = Instantiate(menuItemViewPrefab, new Vector3(312, 384, 0), new Quaternion(0, 0, 0, 0)).GetComponent<MenuItemView>();
                itemView.transform.SetParent(CanvasGetter.getCanvas().transform);
            }

            foreach(var character in party.getParty()){
                Debug.Log(character.getName());
            }

            itemView.setItem(item,party,this);
            
        }

		/// <summary>
		/// スキルをスクロールビューに表示させます
		/// </summary>
		public void inputSkills() {
			selectviewContainer.detach();
			deleteCursors();

            List<ISkill> skills = new List<ISkill>();
            skills.AddRange(player.getActiveSkills());
            skills.AddRange(player.getReactionSkills());

            List<MenuSkillNode> skillNodes = new List<MenuSkillNode>();
            foreach(ISkill skill in skills){
                MenuSkillNode skillNode = Instantiate(menuSkillNodePrefab).GetComponent<MenuSkillNode>();
                skillNode.setSkill(skill);
                skillNodes.Add(skillNode);
            }

            skillSelectView = selectviewContainer.creatSelectView<MenuSkillNode, ISkill>(skillNodes);
            try {
                inputSkillView(skillSelectView.getElement());
            }catch{
                Debug.Log("there are no skills");
            }
        }

        /// <summary>
        /// スキル選択時の処理
        /// </summary>
        /// <param name="skill">選択されたスキル</param>
        public void inputSkillView(ISkill skill){
            if (skillView == null) {
                skillView = Instantiate(menuSkillViewPrefab, new Vector3(312, 384, 0), new Quaternion(0, 0, 0, 0)).GetComponent<MenuSkillView>();
                skillView.transform.SetParent(CanvasGetter.getCanvas().transform);
            }

            skillView.printSkill(skill,this);
        }

        private void deleteCursors(){
            if (skillSelectView != null)
                skillSelectView.delete();

            if (itemSelectView != null)
                itemSelectView.delete();

            if (indexSelectView != null)
                indexSelectView.delete();

            if (characterSelectView != null)
                characterSelectView.delete();
        }

        /// <summary>
        /// 戻るが選択された時の処理
        /// </summary>
        public void backChose(){
            if (skillView != null)
                Destroy(skillView.gameObject);
            skillView = null;

            if (itemView != null)
                Destroy(itemView.gameObject);
            itemView = null;

            if (stateView != null)
                Destroy(stateView.gameObject);
            stateView = null;

            inputIndex();
        }

        /// <summary>
        /// 終了が選択された時の処理
        /// </summary>
        public void finishChose(){
            Debug.Log("into finish");

            if(skillView != null )
                Destroy(skillView.gameObject);

            if (itemView != null)
                Destroy(itemView.gameObject);

            selectviewContainer.detach();
            Destroy(this.gameObject);
        }

        public void cancelChose(){
            Debug.Log(currentContent);
            if(currentContent == MenuContents.INDEX){
                finishChose();
            }else{
                backChose();
            }
        }
    }
}
