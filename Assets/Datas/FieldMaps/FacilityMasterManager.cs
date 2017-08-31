using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;

namespace MasterData {
    public class FacilityMasterManager : MasterDataManagerBase{
        private readonly static FacilityMasterManager INSTANCE = new FacilityMasterManager();

		private FacilityMasterManager() {
			var csv = (TextAsset)Resources.Load("MasterDatas/FacilityMasterData");
			constractedBehaviour(csv);
            
        }

        public static FacilityMasterManager getInstance(){
            return INSTANCE;
        }

        private List<FacilityBuilder> dataTable = new List<FacilityBuilder>();

        public FacilityBuilder getShopFromId(int id){
            foreach(FacilityBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder;
            }
            throw new ArgumentException("invalid shopId");
        }

        public List<int> getLevelShopsId(int level){
            var ids = new List<int>();
            foreach(FacilityBuilder builder in dataTable){
                if (builder.getLevel() <= level)
                    ids.Add(builder.getId());
            }
            return ids;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new FacilityBuilder(datas));
        }
    }
}
