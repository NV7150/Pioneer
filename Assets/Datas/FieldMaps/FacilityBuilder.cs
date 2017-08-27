using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using FieldMap;

using FriendlyCharacterType = Parameter.CharacterParameters.FriendlyCharacterType;

namespace MasterData {
    public class FacilityBuilder {
        private readonly int
        ID,
        OWNER_ID,
        LEVEL;

        private readonly string
        NAME,
        MODEL_ID;

        private readonly FriendlyCharacterType OWNER_TYPE;

        public FacilityBuilder(string[] datas) {
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            LEVEL = int.Parse(datas[2]);
            OWNER_ID = int.Parse(datas[3]);
            MODEL_ID = datas[4];
            OWNER_TYPE = (FriendlyCharacterType)Enum.Parse(typeof(FriendlyCharacterType), datas[5]);
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

        public IFriendly creatOwner(Town livingTown){
            return FriendlyCharacterHelper.getFacilityCaracter(OWNER_ID, OWNER_TYPE, livingTown);
        }
    }
}
