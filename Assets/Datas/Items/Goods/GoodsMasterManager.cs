using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class GoodsMasterManager : MasterDataManagerBase{
        private static List<Goods> dataTable = new List<Goods>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/GoodsMasterData");
            constractedBehaviour(csv);
        }

        public static Goods getGoodsFromId(int id){
            foreach(Goods goods in dataTable){
                if(goods.getId() == id)
                    return goods;
            }
            throw new ArgumentException("invalid goodsId");
        }

		protected override void addInstance(string[] datas) {
            dataTable.Add(new Goods(datas));
		}
    }
}
