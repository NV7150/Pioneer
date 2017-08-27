using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;

namespace MasterData {
    public class HouseMasterManager : MasterDataManagerBase {
        private static readonly HouseMasterManager INSTANCE = new HouseMasterManager();

        public static HouseMasterManager getInstance(){
            return INSTANCE;
        }

		private HouseMasterManager() {
			var csv = (TextAsset)Resources.Load("MasterDatas/HouseMasterData");
			constractedBehaviour(csv);
        }

        private List<HouseBuilder> dataTable = new List<HouseBuilder>();

        public HouseBuilder getHouseBuilderFromId(int id){
            foreach(HouseBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder;
            }
            throw new ArgumentException("invalid buildingId");
        }

        public int getNumberOfHouse(){
            return dataTable.Count;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new HouseBuilder(datas));
        }
    }
}