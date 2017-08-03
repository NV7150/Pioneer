using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class ItemMaterialMasterManager : MasterDataManagerBase {
        private static List<ItemMaterialBuilder> dataTable = new List<ItemMaterialBuilder>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/ItemMaterialMasterData");
            constractedBehaviour(csv);
        }

        public static ItemMaterial getMaterialFromId(int id){
            foreach(ItemMaterialBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder.build();
            }

            throw new ArgumentException("invalid MaterialId");
        }

        public static List<ItemMaterial> getMaterialFromLevel(int level){
            var materials = new List<ItemMaterial>();
            foreach(ItemMaterialBuilder builder in dataTable){
                if (builder.getLevel() == level)
                    materials.Add(builder.build());
            }
            return materials;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new ItemMaterialBuilder(datas));
        }
    }
}