using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class ItemMaterialBuilder {
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

        public ItemMaterialBuilder(string[] datas) {
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            QUALITY = int.Parse(datas[2]);
            MASS = int.Parse(datas[3]);
            VALUE = int.Parse(datas[4]);
            CONSUMABILITY = float.Parse(datas[5]);
            LEVEL = int.Parse(datas[6]);
            HEAVINESS = int.Parse(datas[7]);
            DESCRIPTOIN = datas[8];
            FLAVOR_TEXT = datas[9];
            ADDITIONAL_DESCRIPTION = datas[10];
			ADDITIONAL_FLAVOR = datas[11];
        }

        public int getQuality() {
            return QUALITY;
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

        public int getHeaviness(){
            return HEAVINESS;
        }

        public string getAdditionalDescription(){
            return ADDITIONAL_DESCRIPTION;
        }

        public string getAdditionalFlavor(){
            return ADDITIONAL_FLAVOR;
        }

        public ItemMaterial build(){
            return new ItemMaterial(this);
        }
    }
}