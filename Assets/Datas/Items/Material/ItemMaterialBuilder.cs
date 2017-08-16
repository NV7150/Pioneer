using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;
using ItemAttribute = Item.ItemParameters.ItemAttribute;

namespace MasterData {
    public class ItemMaterialBuilder {
        private readonly int
            ID,
            MASS,
            HEAVINESS,
            RAW_ITEM_VALUE;

		private readonly float
            CONSUMABILITY,
            RAW_QUALITY;

        private int
	        level,
	        itemValue;

            

        private float
            quality;

        private readonly string
            NAME,
            DESCRIPTOIN,
            FLAVOR_TEXT,
	        ADDITIONAL_DESCRIPTION,
	        ADDITIONAL_FLAVOR;

        private readonly ItemAttribute ITEM_ATTRIBUTE;

        public ItemMaterialBuilder(string[] datas) {
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            quality = float.Parse(datas[2]);
            RAW_QUALITY = quality;
            MASS = int.Parse(datas[3]);
            itemValue = int.Parse(datas[4]);
            RAW_ITEM_VALUE = itemValue;
            CONSUMABILITY = float.Parse(datas[5]);
            level = int.Parse(datas[6]);
            HEAVINESS = int.Parse(datas[7]);
            ITEM_ATTRIBUTE = (ItemAttribute)System.Enum.Parse(typeof(ItemAttribute), datas[8]);
            DESCRIPTOIN = datas[9];
            FLAVOR_TEXT = datas[10];
            ADDITIONAL_DESCRIPTION = datas[11];
			ADDITIONAL_FLAVOR = datas[12];
        }

        public float getQuality() {
            return quality;
        }

        public float getConsumability() {
            return CONSUMABILITY;
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
            return itemValue;
        }

        public int getMass() {
            return MASS;
        }

        public string getName() {
            return NAME;
        }

        public int getLevel(){
            return level;
        }

        public int getHeaviness(){
            return HEAVINESS;
        }

        public string getAdditionalDescription(){
            return ADDITIONAL_DESCRIPTION;
        }

        public string getAdditionalFlavor(){
            return ADDITIONAL_FLAVOR;
        }

        public ItemAttribute getItemAttribute(){
            return ITEM_ATTRIBUTE;
        }

        public float getRawQuality(){
            return RAW_QUALITY;
        }

        public int getRawItemValue(){
            return RAW_ITEM_VALUE;
        }

        public ItemMaterial build(){
            return new ItemMaterial(this);
        }

        public void addProgress(ItemMaterialProgress progress){
            this.quality = RAW_QUALITY +  progress.Quality;
            this.itemValue = RAW_ITEM_VALUE + progress.ItemValue;
            this.level = progress.Level;
        }
    }
}