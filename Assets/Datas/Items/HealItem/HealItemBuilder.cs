using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Item;

using HealAttribute = Skill.ActiveSkillParameters.HealSkillAttribute;
using ItemAttribute = Item.ItemParameters.ItemAttribute;

namespace MasterData{
    public class HealItemBuilder {
        private readonly int
            /// <summary> アイテムID </summary>
            ID,
            /// <summary> アイテムの重量 </summary>
            MASS,
	        RAW_HEAL,
	        RAW_ITEM_VALUE;

        private int
	        heal,
	        level,
	        itemValue;

		private readonly string
			/// <summary> アイテム名 </summary>
            NAME,
			/// <summary> アイテムの説明文 </summary>
            DESCRIPTION,
			/// <summary> アイテムのフレーバーテキスト </summary>
            FLAVOR_TEXT;


		/// <summary> アイテムの回復属性 </summary>
		private readonly HealAttribute ATTRIBUTE;

        private readonly ItemAttribute ITEM_ATTRIBUTE;

        private readonly HealItemObserver observer;

        /// <summary>
        /// コンストラクタ
        /// csvから初期化します
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
        public HealItemBuilder(string[] datas){
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            heal = int.Parse(datas[2]);
            RAW_HEAL = heal;            
            itemValue = int.Parse(datas[3]);
            RAW_ITEM_VALUE = itemValue; 
			MASS = int.Parse(datas[4]);
			level = int.Parse(datas[5]);
            ATTRIBUTE = (HealAttribute)Enum.Parse(typeof(HealAttribute),datas[6]);
            ITEM_ATTRIBUTE = (ItemAttribute)System.Enum.Parse(typeof(ItemAttribute), datas[7]);
            DESCRIPTION = datas[8];
            FLAVOR_TEXT = datas[9];

            observer = new HealItemObserver(ID);
        }

		public int getHeal() {
			return heal;
		}

		public HealAttribute getAttribute() {
			return ATTRIBUTE;
		}

		public string getDescription() {
			return DESCRIPTION;
		}

		public int getId() {
			return ID;
		}

		public int getItemValue() {
			return itemValue;
		}

		public int getMass() {
			return MASS;
		}

		public string getName() {
			return NAME;
		}

		public string getFlavorText() {
			return FLAVOR_TEXT;
		}

		public int getLevel() {
			return level;
		}

        public int getRawHeal(){
            return RAW_HEAL;
        }

        public int getRawItemValue(){
            return RAW_ITEM_VALUE;
        }

        public HealItem build(){
            return new HealItem(this,observer);
        }

        public ItemAttribute getItemAttribute(){
            return ITEM_ATTRIBUTE;
        }

        public void addProgress(HealItemProgress progress){
            this.heal = RAW_HEAL + progress.Heal;
            this.itemValue = RAW_ITEM_VALUE + progress.ItemValue;
            this.level = progress.Level;
        }
    }
}
