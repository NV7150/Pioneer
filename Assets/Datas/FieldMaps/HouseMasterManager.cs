using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;

namespace MasterData {
    public class HouseMasterManager : MasterDataManagerBase {
        private static List<HouseBuilder> dataTable = new List<HouseBuilder>();

		private void Awake() {
			var csv = (TextAsset)Resources.Load("MasterDatas/HouseMasterData");
			constractedBehaviour(csv);
		}

        public static HouseBuilder getHouseBuilderFromId(int id){
            foreach(HouseBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder;
            }
            throw new ArgumentException("invalid buildingId");
        }

        public static int getNumberOfHouse(){
            return dataTable.Count;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new HouseBuilder(datas));
        }
    }
}