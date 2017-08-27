using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class TradeItemMasterManager : MasterDataManagerBase {
        private static readonly TradeItemMasterManager INSTANCE = new TradeItemMasterManager();

        private TradeItemMasterManager(){
            var csv = (TextAsset)Resources.Load("MasterDatas/TradeItemMasterData");
            constractedBehaviour(csv);
        }

        public static TradeItemMasterManager getInstance(){
            return INSTANCE;
        }

        private List<TradeItemBuilder> dataTable = new List<TradeItemBuilder>();

        protected override void addInstance(string[] datas) {
            dataTable.Add(new TradeItemBuilder(datas));
        }

        public TradeItem getTradeItemFromId(int id){
            foreach(TradeItemBuilder builder in dataTable){
                if (builder.Id == id)
                    return builder.build();
            }
            throw new ArgumentException("invalid tradeItemID");
        }

        public List<TradeItem> getTradeItemsFromLevel(int level){
            List<TradeItem> tradeItems = new List<TradeItem>();
            foreach(TradeItemBuilder builder in dataTable){
                if (builder.Level <= level && (builder.Level - level) <= 3)
                    tradeItems.Add(builder.build());
            }

            return tradeItems;
        }
    }
}