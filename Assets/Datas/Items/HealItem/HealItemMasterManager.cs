using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class HealItemMasterManager : MasterDataManagerBase {
        private static List<HealItemBuilder> dataTable = new List<HealItemBuilder>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/HealItemMasterData");
            constractedBehaviour(csv);
        }

        public static HealItem getHealItemFromId(int id){
            foreach(HealItemBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder.build();
            }
            throw new ArgumentException("invalid healItemId");
        }


        public static List<HealItem> getHealItemsFromLevel(int level){
            var items = new List<HealItem>();
            foreach(HealItemBuilder builder in dataTable){
                if (builder.getLevel() == level)
                    items.Add(builder.build());
            }
            return items;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new HealItemBuilder(datas));
        }
    }
}
