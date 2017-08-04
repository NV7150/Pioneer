using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;

namespace MasterData {
    public class TownAttributeMasterManager : MasterDataManagerBase {
        private static List<TownAttribute> dataTable = new List<TownAttribute>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/TownAttributeMasterData");
            constractedBehaviour(csv);
        }

        public static TownAttribute getTownAttributeFromId(int id){
            foreach(TownAttribute attribute in dataTable){
                if (attribute.getId() == id)
                    return attribute;
            }
            throw new ArgumentException("invalid townAttributeId");
        }

        public static TownAttribute getRandomAttribute(){
            int rand = UnityEngine.Random.Range(0, dataTable.Count);
            return dataTable[rand];
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new TownAttribute(datas));
        }
    }
}