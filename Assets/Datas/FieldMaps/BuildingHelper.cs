using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MasterData;

namespace FieldMap {
    public static class BuildingHelper{
        public static HouseBuilder getRandomHouse(){
            int rand = Random.Range(0, HouseMasterManager.getNumberOfHouse());
            return HouseMasterManager.getHouseBuilderFromId(rand);
        }

        public static ShopBuilder getRandomLevelShop(int level){
            //かり
            level = 1;
            var randomList = ShopMasterManager.getLevelShopsId(level);
            int rand = Random.Range(0,randomList.Count);
            return ShopMasterManager.getShopFromId(randomList[rand]);
        }
    }
}
