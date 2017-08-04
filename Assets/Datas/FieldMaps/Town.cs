using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using MasterData;

using ItemAttribute = Item.ItemParameters.ItemAttribute;

namespace FieldMap {
    public class Town : MonoBehaviour {
        float priseMag;
        int level;
        int size;
        List<IFriendly> characters = new List<IFriendly>();
        List<Building> buildings = new List<Building>();
		/// <summary>
		/// 建物の埋まり具合を表します
        /// 座標10*10を1グリッドとし、便宜上１つ目のインデックスをx(横)、２つ目のインデックスをz(縦)とします
		/// </summary>
		bool[,] grid = new bool[10, 10];

        private readonly int GRID_SIZE = 10;

        int loadPosX = 5;
        int loadPosZ = 0;
        RoadDirection direction = RoadDirection.VERTICAL;

        private GameObject roadPrefab;

        private TownAttribute attribute;

        private void Awake() {
            roadPrefab = (GameObject)Resources.Load("Models/road");
        }

        public void setState(int level,int size){
            this.level = level;
            this.size = size;
            layRoad();
            buildBuildings();

            this.attribute = TownAttributeMasterManager.getRandomAttribute();
            priseMag = attribute.getPriseMag() * UnityEngine.Random.Range(0.8f, 1.2f) + (level - size) / 100; 
            Debug.Log(attribute.getName());
        }

        private void layRoad(){
            Instantiate(roadPrefab,transform.position,new Quaternion(0,0,0,0));
            for (int i = 0; i < 10; i++){
                grid[4, i] = true;
                grid[5, i] = true;
                grid[6, i] = true;
            }
        }

        private bool judgeBuild(int x,int z){
            if (!grid[x, z]) {
                bool isXAlongRoad = direction == RoadDirection.HORIZONTAL || Math.Abs(x - loadPosX) <= 2;
                bool isZAlongRoad = direction == RoadDirection.VERTICAL || Math.Abs(z - loadPosZ) <= 2;
                if (isXAlongRoad && isZAlongRoad) {
                    int rand = UnityEngine.Random.Range(0, 5);
                    return rand == 0;
                }
            }
            return false;
        }

        private void buildBuildings(){
            int buildingMax = size + 2;

            for (int x = 0; x < grid.GetLength(0) && buildings.Count < buildingMax;x++){
                for (int z = 0; z < grid.GetLength(1) && buildings.Count < buildingMax; z++){
                    if(judgeBuild(x,z)){
                        float xPosAdds = (x - grid.GetLength(0) / 2) * GRID_SIZE + GRID_SIZE / 2;
                        float zPosAdds = (z - grid.GetLength(1) / 2) * GRID_SIZE + GRID_SIZE / 2;
						Vector3 pos = transform.position + new Vector3(xPosAdds, 0, zPosAdds);

						int rand = UnityEngine.Random.Range(0, 2);
                        Building building;
                        if(rand == 0){
                            building = BuildingHelper.getRandomHouse(pos);
                            characters.Add(building.getOwnerCharacter());
                            buildings.Add(building);
                        }else{
                            building = BuildingHelper.getRandomLevelShop(level,pos,this);
                            characters.Add(building.getOwnerCharacter());
                            buildings.Add(building);
                        }
                        if(x > 5){
                            building.transform.Rotate(new Vector3(0,180,0));
                        }

                        fillGrid(x,z);
                    }
                }
            }

        }

        private void fillGrid(int x,int z){
			for (int i = -1; i < 2; i++) {
				for (int j = -1; j < 2; j++) {
                    if ((x + i) >= 0 && (x + i) < grid.GetLength(0) && (z + j) >= 0 && (z + j) < grid.GetLength(1))
                        grid[x + i, z + j] = true;
				}
			}
        }

        public float getItemValueMag(ItemAttribute itemAttribute){
            return priseMag * attribute.getAttributeMag(itemAttribute);
        }

        // Update is called once per frame
        void Update() {

        }

        private enum RoadDirection{
            HORIZONTAL,
            VERTICAL
        }
    }
}
