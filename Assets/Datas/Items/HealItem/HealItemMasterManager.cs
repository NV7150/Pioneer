﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class HealItemMasterManager : MasterDataManagerBase {
        private static List<HealItemBuilder> dataTable = new List<HealItemBuilder>();
        private static Dictionary<int,HealItemProgress> progressTable = new Dictionary<int, HealItemProgress>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/HealItemMasterData");
			constractedBehaviour(csv);
			updateProgress();
        }

        public static HealItem getHealItemFromId(int id){
            foreach(HealItemBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder.build();
            }
            throw new ArgumentException("invalid healItemId");
        }

		public static HealItemBuilder getHealItemBuilderFromId(int id) {
			foreach (HealItemBuilder builder in dataTable) {
				if (builder.getId() == id)
                    return builder;
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
            var builder = new HealItemBuilder(datas);
            dataTable.Add(builder);
            int id = int.Parse(datas[0]);
            progressTable.Add(int.Parse(datas[0]),new HealItemProgress());
        }

        public static void updateProgress(){
            foreach(HealItemBuilder builder in dataTable){
                int id = builder.getId();
                if (ES2.Exists(getLoadPass(id,"HealItemProgress"))) {
                    Debug.Log("into load");
					var progress = loadSaveData<HealItemProgress>(id, "HealItemProgress");
					builder.addProgress(progress);
                    progressTable[id] = progress;
                }
            }
        }

        public static HealItemProgress getProgress(int id){
            return progressTable[id];
        }

    }
}
