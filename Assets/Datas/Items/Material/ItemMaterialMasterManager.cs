using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class ItemMaterialMasterManager : MasterDataManagerBase {
        private static readonly ItemMaterialMasterManager INSTACE = new ItemMaterialMasterManager();
        public static ItemMaterialMasterManager getInstance(){
            return INSTACE;
        }

		private ItemMaterialMasterManager() {
			var csv = (TextAsset)Resources.Load("MasterDatas/ItemMaterialMasterData");
			constractedBehaviour(csv);
        }

        private List<ItemMaterialBuilder> dataTable = new List<ItemMaterialBuilder>();
        public Dictionary<int, ItemMaterialProgress> progressTable = new Dictionary<int, ItemMaterialProgress>();

        public ItemMaterial getMaterialFromId(int id){
            foreach(ItemMaterialBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder.build();
            }

            throw new ArgumentException("invalid MaterialId");
        }

        public ItemMaterialBuilder getMaterialBuilderFromId(int id) {
			foreach (ItemMaterialBuilder builder in dataTable) {
				if (builder.getId() == id)
					return builder;
			}

			throw new ArgumentException("invalid MaterialId");
		}

        public List<ItemMaterial> getMaterialFromLevel(int level){
            var materials = new List<ItemMaterial>();
            foreach(ItemMaterialBuilder builder in dataTable){
                if (builder.getLevel() <= level)
                    materials.Add(builder.build());
            }
            return materials;
        }

        protected override void addInstance(string[] datas) {
            var builder = new ItemMaterialBuilder(datas);
            dataTable.Add(builder);
            progressTable.Add(int.Parse(datas[0]),new ItemMaterialProgress());
        }

        public void addProgress(int worldId){
            foreach (ItemMaterialBuilder builder in dataTable) {
                int id = builder.getId();
                if (ES2.Exists(getLoadPass(id,worldId, "ItemProgress"))) {
                    var progress = loadSaveData<ItemMaterialProgress>(id,worldId, "ItemProgress");
                    builder.addProgress(progress);
                    progressTable[id] = progress;
                }
            }
        }

        public ItemMaterialProgress getProgress(int id){
            return progressTable[id];
        }
    }
}