using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class ItemMaterialMasterManager : MasterDataManagerBase {
        private static List<ItemMaterialBuilder> dataTable = new List<ItemMaterialBuilder>();
        public static Dictionary<int, ItemMaterialProgress> progressTable = new Dictionary<int, ItemMaterialProgress>();

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

        public static ItemMaterialBuilder getMaterialBuilderFromId(int id) {
			foreach (ItemMaterialBuilder builder in dataTable) {
				if (builder.getId() == id)
					return builder;
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
            var builder = new ItemMaterialBuilder(datas);
            dataTable.Add(builder);
        }

        public static void updateProgress(){
            foreach (ItemMaterialBuilder builder in dataTable) {
                int id = builder.getId();
                if (ES2.Exists(getLoadPass(id, "ItemProgress"))) {
                    var progress = loadSaveData<ItemMaterialProgress>(id, "ItemProgress");
                    builder.addProgress(progress);
                    progressTable.Add(id, progress);
                }
            }
        }

        public static ItemMaterialProgress getProgress(int id){
            return progressTable[id];
        }
    }
}