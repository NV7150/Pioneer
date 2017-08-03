using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MasterData;

namespace FieldMap {
    public static class BuildingHelper{
        public static Building getRandomHouse(Vector3 pos){
            int rand = Random.Range(0, HouseMasterManager.getNumberOfHouse());
            return HouseMasterManager.getHouseFromId(rand,pos);
        }

        public static Building getRandomLevelShop(int level,Vector3 pos){
            //かり
            level = 1;
            var randomList = ShopMasterManager.getLevelShopsId(level);
            int rand = Random.Range(0,randomList.Count);
            return ShopMasterManager.getShopFromId(randomList[rand], pos);
        }
    }
}
