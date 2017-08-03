using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

using MasterData;

using ItemType = Item.ItemParameters.ItemType;
using static Item.ItemParameters.ItemType;

namespace Item {
    public class ItemMaterial : IItem {
        private readonly int
	        ID,
	        QUALITY,
	        MASS,
	        VALUE,
            LEVEL,
            HEAVINESS;

        private readonly float CONSUMABILITY;

        private readonly string
	        NAME,
	        DESCRIPTOIN,
			FLAVOR_TEXT,
			ADDITIONAL_DESCRIPTION,
			ADDITIONAL_FLAVOR;

        public ItemMaterial(ItemMaterialBuilder builder){
            this.ID = builder.getId();
            this.QUALITY = builder.getQuality();
            this.MASS = builder.getMass();
            this.VALUE = builder.getItemValue();
            this.CONSUMABILITY = builder.getConsumability();
            this.NAME = builder.getName();
            this.DESCRIPTOIN = builder.getDescription();
            this.FLAVOR_TEXT = builder.getFlavorText();
            this.LEVEL = builder.getLevel();
            this.HEAVINESS = builder.getHeaviness();
            this.ADDITIONAL_DESCRIPTION = builder.getAdditionalDescription();
            this.ADDITIONAL_FLAVOR = builder.getAdditionalFlavor();
        }

        public int getQuality(){
            return QUALITY;
        }

        public float getConsumability(){
            return CONSUMABILITY;
        }

        public int getHeaviness(){
            return HEAVINESS;
        }

        public bool getCanStack() {
            return true;
        }

        public bool getCanStore() {
            return true;
        }

        public string getDescription() {
            return DESCRIPTOIN;
        }

        public string getFlavorText() {
            return FLAVOR_TEXT;
        }

        public int getId() {
            return ID;
        }

        public int getItemValue() {
            return VALUE;
        }

        public int getMass() {
            return MASS;
        }

        public string getName() {
            return NAME;
        }

        public int getLevel(){
            return LEVEL;
        }

		public string getAdditionalDescription() {
			return ADDITIONAL_DESCRIPTION;
		}

		public string getAdditionalFlavor() {
			return ADDITIONAL_FLAVOR;
		}

        public void use(IPlayable user) {
            //なんもしない
            //使えないよというメッセージでも表示する
        }

        public ItemType getItemType() {
            return ITEM_MATERIAL;
        }
    }
}
