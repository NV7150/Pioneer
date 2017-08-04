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
			/// <summary> アイテムの回復量 </summary>
			HEAL,
			/// <summary> アイテムの基本価格 </summary>
			ITEM_VALUE,
			/// <summary> アイテムの重量 </summary>
			MASS,
            LEVEL;

		private readonly string
			/// <summary> アイテム名 </summary>
			NAME,
			/// <summary> アイテムの説明文 </summary>
			DESCRITION,
			/// <summary> アイテムのフレーバーテキスト </summary>
			FLAVOR_TEXT;


		/// <summary> アイテムの回復属性 </summary>
		private readonly HealAttribute ATTRIBUTE;

        private readonly ItemAttribute ITEM_ATTRIBUTE;

        /// <summary>
        /// コンストラクタ
        /// csvから初期化します
        /// </summary>
        /// <param name="datas">csvによるstring配列データ</param>
        public HealItemBuilder(string[] datas){
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            HEAL = int.Parse(datas[2]);
            ITEM_VALUE = int.Parse(datas[3]);
			MASS = int.Parse(datas[4]);
			LEVEL = int.Parse(datas[5]);
            ATTRIBUTE = (HealAttribute)Enum.Parse(typeof(HealAttribute),datas[6]);
            ITEM_ATTRIBUTE = (ItemAttribute)System.Enum.Parse(typeof(ItemAttribute), datas[7]);
            DESCRITION = datas[8];
            FLAVOR_TEXT = datas[9];
        }

		public int getHeal() {
			return HEAL;
		}

		public HealAttribute getAttribute() {
			return ATTRIBUTE;
		}

		public string getDescription() {
			return DESCRITION;
		}

		public int getId() {
			return ID;
		}

		public int getItemValue() {
			return ITEM_VALUE;
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
			return LEVEL;
		}

        public HealItem build(){
            return new HealItem(this);
        }

        public ItemAttribute getItemAttribute(){
            return ITEM_ATTRIBUTE;
        }
    }
}
