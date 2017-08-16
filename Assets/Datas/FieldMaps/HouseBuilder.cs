using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using FieldMap;

namespace MasterData {
    public class HouseBuilder {
        private readonly int
        ID,
        CITIZEN_ID;

        private readonly string
        NAME,
        MODEL_ID;

        public HouseBuilder(string[] datas) {
            ID = int.Parse(datas[0]);
            NAME = datas[1];
            CITIZEN_ID = int.Parse(datas[2]);
            MODEL_ID = datas[3];
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

        public Building build(Vector3 position,long id) {
            GameObject buildingObject = MonoBehaviour.Instantiate((GameObject)Resources.Load("Models/" + MODEL_ID));
            var building = buildingObject.GetComponent<Building>();
            building.setState(position,id);
            return building;
        }

        public Citizen creatCitizen(){
            return CitizenMasterManager.getCitizenFromId(CITIZEN_ID);
        }
    }
}