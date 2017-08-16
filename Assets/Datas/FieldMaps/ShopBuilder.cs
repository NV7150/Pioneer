using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using FieldMap;

namespace MasterData {
    public class ShopBuilder {
        private readonly int
        ID,
        MERCHANT_ID,
        LEVEL;

        private readonly string
        NAME,
        MODEL_ID;

        public ShopBuilder(string[] datas) {
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            LEVEL = int.Parse(datas[2]);
            MERCHANT_ID = int.Parse(datas[3]);
            MODEL_ID = datas[4];
        }

        public int getId() {
            return ID;
        }

        public string getName() {
            return NAME;
        }

        public string getModelId() {
            return MODEL_ID;
        }

        public int getLevel(){
            return LEVEL;
        }

        public Building build(Vector3 position,long id) {
            GameObject buildingObject = MonoBehaviour.Instantiate((GameObject)Resources.Load("Models/" + MODEL_ID));
            var building = buildingObject.GetComponent<Building>();
            building.setState(position,id);
            return building;
        }

        public Merchant creatMerchant(Town livingTown){
            return MerchantMasterManager.getMerchantFromId(MERCHANT_ID, livingTown);
        }
    }
}
