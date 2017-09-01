using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Character;
using MasterData;
using Item;

using ItemAttribute = Item.ItemParameters.ItemAttribute;
using Random = UnityEngine.Random;

namespace FieldMap {
    public class Town : MonoBehaviour {
        private int id;

        float priseMag;
        int level;
        int size;
        public List<IFriendly> characters = new List<IFriendly>();
        private List<BuildingSaveData> buildingDatas = new List<BuildingSaveData>();
        private List<Building> buildings = new List<Building>();
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

        private Dictionary<ItemAttribute, float> attributeMag = new Dictionary<ItemAttribute, float>();

        private TownObserver observer;

        private List<Citizen> citizens = new List<Citizen>();
      
        private List<Merchant> merchants = new List<Merchant>();
       
        private List<Client> clients = new List<Client>();

        int creatEnemyCount = 0;

        private void Awake() {
            roadPrefab = (GameObject)Resources.Load("Models/road");
        }

        private void Update() {
            creatEnemyCount++;
            if (creatEnemyCount % 10 == 0) {
                float probality = Random.Range(0, 100);
                if (probality <= 5f) {
                    creatEnemy();
                }
            }
        }

        public void setState(int level, int size, int id){
            this.level = level;
            this.size = size;
            this.id = id;
            layRoad();
            buildBuildings();

            this.attribute = TownAttributeMasterManager.getInstance().getRandomAttribute();
            priseMag = attribute.getPriseMag() * UnityEngine.Random.Range(0.8f, 1.2f) + (level - size) / 100;
            this.attributeMag = attribute.getAttributeMags();

            observer = new TownObserver(this);
        }

        public void setState(TownBuilder builder) {
            gameObject.transform.position = builder.Position;
            gameObject.transform.rotation = builder.Quaternion;

            this.direction = builder.Direction;
            layRoad();

            this.id = builder.Id;
            this.level = builder.Level;
            this.size = builder.Size;
            this.priseMag = builder.PriseMag;
            this.clients = builder.Clients;
            this.merchants = builder.Merchants;
            this.citizens = builder.Citizens;
            this.attributeMag = builder.AttributeMag;
            this.grid = builder.Grid;
            this.buildingDatas = builder.BuildingDatas;

            this.attribute = TownAttributeMasterManager.getInstance().getTownAttributeFromId(builder.TownAttributeId);

            Debug.Log("attributeMag count " + attributeMag.Count);

            foreach(var buildingData in buildingDatas){
                var building = buildingData.restore();
                buildings.Add(building);
                building.transform.SetParent(transform);
            }

            characters.AddRange(citizens);

            foreach(Merchant merchant in merchants)
                merchant.setTown(this);
            characters.AddRange(merchants);

            characters.AddRange(clients);

            foreach (IFriendly friendlyCharacter in characters){
                friendlyCharacter.getContainer().transform.SetParent(transform);
            }

            observer = new TownObserver(this);
        }

        private void layRoad(){
            //Instantiate(roadPrefab,transform.position,new Quaternion(0,0,0,0)).transform.SetParent(transform);
            //for (int i = 0; i < 10; i++){
            //    grid[4, i] = true;
            //    grid[5, i] = true;
            //    grid[6, i] = true;
            //}
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

            for (int x = 0; x < grid.GetLength(0) && buildings.Count < buildingMax; x++) {
                for (int z = 0; z < grid.GetLength(1) && buildings.Count < buildingMax; z++) {
                    creatBuilding(x,z);
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

        private void creatBuilding(int x,int z){
            if (judgeBuild(x, z)) {
                float xPosAdds = (x - grid.GetLength(0) / 2) * GRID_SIZE;
                float zPosAdds = (z - grid.GetLength(1) / 2) * GRID_SIZE;
                Vector3 pos = transform.position + new Vector3(xPosAdds, 0, zPosAdds);

                int rand = UnityEngine.Random.Range(0, 4);
                Building building;
                if (rand == 0) {
                    var builder = BuildingHelper.getRandomHouse();
					var character = builder.creatCitizen();
					building = builder.build(pos, character.getUniqueId());
                    float y = Terrain.activeTerrain.terrainData.GetInterpolatedHeight(
                        pos.x / Terrain.activeTerrain.terrainData.size.x,
                        pos.z / Terrain.activeTerrain.terrainData.size.z
					);
					pos = new Vector3(pos.x, y, pos.z);
					character.getContainer().transform.position = pos;
                    character.getContainer().transform.SetParent(transform);
                    characters.Add(character);
                    citizens.Add(character);
					if (x > 5) {
						building.transform.Rotate(new Vector3(0, 180, 0));
						character.getContainer().transform.Rotate(new Vector3(0, -90, 0));
                    }else{
                        character.getContainer().transform.Rotate(new Vector3(0, 90, 0));
                    }
                    buildingDatas.Add(new BuildingSaveData(builder.getModelId(),building.transform));
                } else {
                    var builder = BuildingHelper.getRandomLevelFacility(level);
                    var character = builder.creatOwner(this);
                    building = builder.build(pos, character.getUniqueId());
					float y = Terrain.activeTerrain.terrainData.GetInterpolatedHeight(
						pos.x / Terrain.activeTerrain.terrainData.size.x,
						pos.z / Terrain.activeTerrain.terrainData.size.z
					);
                    pos = new Vector3(pos.x, y, pos.z);
					character.getContainer().transform.position = pos;
					character.getContainer().transform.SetParent(transform);
                    characters.Add(character);

                    if (character is Merchant) {
                        merchants.Add((Merchant)character);
                    }else if(character is Client){
                        clients.Add((Client)character);
                    }else{
                        throw new InvalidOperationException("character " + character.getName() + " isn't merchent nor client");
                    }
					if (x > 5) {
						building.transform.Rotate(new Vector3(0, 180, 0));
                        character.getContainer().transform.Rotate(new Vector3(0, -90, 0));
					} else {
						character.getContainer().transform.Rotate(new Vector3(0, 90, 0));
                    }
                    buildingDatas.Add(new BuildingSaveData(builder.getModelId(), building.transform));
                }


                building.transform.SetParent(transform);

                fillGrid(x, z);
            }
        }

        private void creatRandomBuilding(){
            bool added = false;

            for (int x = 0; !added; x++) {
                for (int z = 0; !added; z++) {
                    if(judgeBuild(x,z)){
                        creatBuilding(x, z);
                    }
                }
            }
        }

        public float getItemValueMag(ItemAttribute itemAttribute){
            return priseMag * attribute.getAttributeMag(itemAttribute);
        }

        public int getSize(){
            return size;
        }

        public int getLevel(){
            return level;
        }

        public int getId(){
            return id;
        }

        public List<IFriendly> getCharacters(){
            return new List<IFriendly>(characters);
        }

        public float getPriseMag(){
            return priseMag;
        }

        public TownAttribute getAttribute(){
            return attribute;
        }

        public void levelUped(){
            level++;
        }

        public void sizeUped(){
            size++;
            creatRandomBuilding();
        }

        public void traded(IFriendly merchant,int tradedValue,ItemAttribute attribute){
            observer.characterTraded(merchant,tradedValue,attribute);
        }

        public void questCleared(Client client){
            observer.characterQuestCleared(client);
        }

        public TownBuilder compressIntoBuilder(){
            var builder = new TownBuilder();
            builder.Id = id;
            builder.Level = level;
            builder.Merchants = new List<Merchant>(merchants);
            builder.PriseMag = priseMag;
            builder.Clients = new List<Client>(clients);
            builder.Citizens = new List<Citizen>(citizens);
            builder.Size = size;
            builder.AttributeMag = new Dictionary<ItemAttribute, float>(attributeMag);
            builder.Position = transform.position;
            builder.Quaternion = transform.rotation;
            builder.TownAttributeId = attribute.getId();
            builder.Direction = this.direction;
            builder.BuildingDatas = this.buildingDatas;
            Array.Copy(grid,builder.Grid,grid.Length);

            return builder;
        }

        private void creatEnemy(){
            int enemyId = EnemyHelper.getRandomEnemyFromLevel(level);

            int blankRand = Random.Range(0, 2);
            float xBlank = (blankRand == 0) ? 50 : 0;
            float zBlank = (blankRand == 1) ? 50 : 0;

            float xPos = Random.Range(0, 50);
            int xRand = (Random.Range(0, 2) == 0) ? 1:-1;
            xPos = (xPos + xBlank) * xRand;
            xPos += transform.position.x;

            float zPos = Random.Range(0, 50);
            int zRand = (Random.Range(0, 2) == 0) ? 1 : -1;
            zPos = (zPos + zBlank) * zRand;
            zPos += transform.position.z;

            float yPos = Terrain.activeTerrain.terrainData.GetInterpolatedHeight(
                xPos / Terrain.activeTerrain.terrainData.size.x,
                zPos / Terrain.activeTerrain.terrainData.size.z
            );

            var enemy = EnemyMasterManager.getInstance().getEnemyFromId(enemyId);
            enemy.getContainer().transform.position = new Vector3(xPos, yPos, zPos);
        }

        public enum RoadDirection{
            HORIZONTAL,
            VERTICAL
        }
    }
}
