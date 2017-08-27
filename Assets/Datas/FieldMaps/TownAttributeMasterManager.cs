using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;

namespace MasterData {
    public class TownAttributeMasterManager : MasterDataManagerBase {
        private static readonly TownAttributeMasterManager INSTANCE = new TownAttributeMasterManager();

        public static TownAttributeMasterManager getInstance(){
            return INSTANCE;
        }

		private TownAttributeMasterManager() {
			var csv = (TextAsset)Resources.Load("MasterDatas/TownAttributeMasterData");
			constractedBehaviour(csv);
        }

        private List<TownAttribute> dataTable = new List<TownAttribute>();

        public TownAttribute getTownAttributeFromId(int id){
            foreach(TownAttribute attribute in dataTable){
                if (attribute.getId() == id)
                    return attribute;
            }
            throw new ArgumentException("invalid townAttributeId");
        }

        public TownAttribute getRandomAttribute(){
            int rand = UnityEngine.Random.Range(0, dataTable.Count);
            return dataTable[rand];
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new TownAttribute(datas));
        }
    }
}