using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FieldMap;

namespace MasterData {
    public class ShopMasterManager : MasterDataManagerBase{
        private static List<ShopBuilder> dataTable = new List<ShopBuilder>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/ShopMasterData");
            constractedBehaviour(csv);
        }

        public static Building getShopFromId(int id,Vector3 pos){
            foreach(ShopBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder.build(pos);
            }
            throw new ArgumentException("invalid shopId");
        }

        public static List<int> getLevelShopsId(int level){
            var ids = new List<int>();
            Debug.Log("lv" + level);
            foreach(ShopBuilder builder in dataTable){
                if (builder.getLevel() == level)
                    ids.Add(builder.getId());
            }
            Debug.Log(ids.Count + "" + dataTable.Count);
            return ids;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new ShopBuilder(datas));
        }
    }
}
