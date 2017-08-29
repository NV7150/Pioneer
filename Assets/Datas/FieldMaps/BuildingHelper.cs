using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MasterData;

namespace FieldMap {
    public static class BuildingHelper{
        public static HouseBuilder getRandomHouse(){
            int rand = Random.Range(0, HouseMasterManager.getInstance().getNumberOfHouse());
            return HouseMasterManager.getInstance().getHouseBuilderFromId(rand);
        }

        public static FacilityBuilder getRandomLevelFacility(int level){
            var randomList = FacilityMasterManager.getInstance().getLevelShopsId(level);
            int rand = Random.Range(0,randomList.Count);
            return FacilityMasterManager.getInstance().getShopFromId(randomList[rand]);
        }
    }
}
