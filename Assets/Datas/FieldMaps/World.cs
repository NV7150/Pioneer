using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MasterData;

namespace FieldMap {
    public class World : MonoBehaviour {
        private int id = 0;
        public List<Transform> townPositions;
        private GameObject townPrefab;
        public Transform missionClientPos;
        public Terrain terrain;

        private Dictionary<int, Vector3> towns = new Dictionary<int, Vector3>();
        private List<Town> enableTowns = new List<Town>();

        private GameObject fieldKeeper;

        private WorldObserver observer;

        private int worldLevel;

        private void Awake() {
			townPrefab = (GameObject)Resources.Load("Models/Town");
			fieldKeeper = GameObject.Find("FieldKeeper");
        }

        private void Start() {
            observer = new WorldObserver(this);
            if(!WorldCreator.getInstance().getWorldLoaded()){
                if(WorldCreator.getInstance().getIsLoad()){
                    id = WorldCreator.getInstance().getLoadWorldId();
                    loadWorld();
                    WorldCreator.getInstance().loadProgresses(id);
                }else{
                    id = WorldCreator.getInstance().getWorldIdDefault() + 1;
                    WorldCreator.getInstance().setWorldIdDefault(id);
                    creatWorld();
                    WorldCreator.getInstance().setWorldPass(id);
                }

                WorldCreator.getInstance().setWorldLoaded(true);
                WorldCreator.getInstance().activetePlayer(missionClientPos);
            }else{
                WorldCreator.getInstance().resetPlayerPos();
                WorldCreator.getInstance().getPlayer().moveEnable();
            }

        }

        public void creatWorld() {
            this.worldLevel = 1;

            int townNumberMin = (townPositions.Count > 5) ? townPositions.Count - 5 : 1;
            int numberOfTown = Random.Range(townNumberMin, townPositions.Count);
            int lowTown = numberOfTown / 3;
            int middleTown = numberOfTown / 3;
            int highTown = numberOfTown / 3;
            for (int i = 0; i < numberOfTown % 3; i++){
                int rand = Random.Range(0,3);
                switch(rand){
                    case 0 : 
                        lowTown++;
                        break;
                    case 1 :
                        middleTown++;
                        break;
                    case 2 :
                        highTown++;
                        break;
                }
            }

            var positoins = shufflePositoin();
            var levels = shuffleTownLevel(lowTown,middleTown,highTown);
			var sizes = shuffleTownSize(lowTown, middleTown, highTown);

            int townId = 0;
            for (int i = 0; i < numberOfTown;i++){
                GameObject townObject = Instantiate(townPrefab, positoins[i],new Quaternion(0, 0, 0, 0));
                Town town = townObject.GetComponent<Town>();
                town.setState(getLevel(levels[i]),getSize(sizes[i]),townId);
                enableTowns.Add(town);
                town.transform.SetParent(fieldKeeper.transform);
                towns.Add(town.getId(),positoins[i]);
                townId++;
            }
        }

		private List<Vector3> shufflePositoin() {
			List<Vector3> positions = new List<Vector3>();
            foreach (Transform transfrom in townPositions)
                positions.Add(transfrom.position);
            int index = positions.Count - 1;
            while(index > 1){
                Vector3 indexPosition = positions[index];
                int rand = Random.Range(0, index + 1);
                positions[index] = positions[rand];
                positions[rand] = indexPosition;
                index--;
            }
            return positions;
        }

        private List<TownLevelDigest> shuffleTownLevel(int low,int middle,int high){
            List<TownLevelDigest> levels = new List<TownLevelDigest>();
            for (int i = 0; i < low;i++)
                levels.Add(TownLevelDigest.LOW);
            for (int i = 0; i < middle; i++)
                levels.Add(TownLevelDigest.MIDDLE);
            for (int i = 0; i < high; i++)
                levels.Add(TownLevelDigest.HIGH);
            
			int index = low + middle + high - 1;
            while(index > 1){
                TownLevelDigest level = levels[index];
				int rand = Random.Range(0, index + 1);
                levels[index] = levels[rand];
                levels[rand] = level;
				index--;
            }
            return levels;
        }

        private List<TownSizeDigest> shuffleTownSize(int little, int midium, int big) {
            List<TownSizeDigest> sizes = new List<TownSizeDigest>();
            for (int i = 0; i < little; i++)
                sizes.Add(TownSizeDigest.SMALL);
            for (int i = 0; i < midium; i++)
                sizes.Add(TownSizeDigest.MEDIUM);
            for (int i = 0; i < big; i++)
                sizes.Add(TownSizeDigest.BIG);

            int index = little + midium + big - 1;
			while (index > 1) {
                TownSizeDigest size = sizes[index];
				int rand = Random.Range(0, index + 1);
				sizes[index] = sizes[rand];
                sizes[rand] = size;
				index--;
			}

			return sizes;
		}

        private int getLevel(TownLevelDigest digest){
            switch(digest){
                case TownLevelDigest.LOW:
                    return Random.Range(1, 8);
                case TownLevelDigest.MIDDLE:
                    return Random.Range(8, 16);
                case TownLevelDigest.HIGH:
                    return Random.Range(16, 25);
            }
            throw new System.ArgumentException("unkonwn levelDigest");
        }

		private int getSize(TownSizeDigest digest) {
			switch (digest) {
                case TownSizeDigest.SMALL:
					return Random.Range(1, 8);
                case TownSizeDigest.MEDIUM:
					return Random.Range(8, 16);
                case TownSizeDigest.BIG:
					return Random.Range(16, 25);
			}
			throw new System.ArgumentException("unkonwn sizeDigest");
		}

        public Town getTownFromId(int id){
            foreach(Town town in enableTowns){
                if (town.getId() == id)
                    return town;
            }
            throw new System.ArgumentException("unkown townId");
        }

        public void loadWorld(){
            var data = MasterDataManagerBase.loadSaveData<WorldData>(id,id,"WorldData");
			this.towns = data.Towns;
            this.worldLevel = data.WorldLevel;

            var ids = towns.Keys;
            foreach(int id in ids){
                var townBuilder = MasterDataManagerBase.loadSaveData<TownBuilder>(id,this.id, "TownData");
                GameObject townObject = Instantiate(townPrefab);
				Town town = townObject.GetComponent<Town>();
                town.setState(townBuilder);
                town.transform.SetParent(fieldKeeper.transform);
                enableTowns.Add(town);
            }
        }

        public void saveWorld(){
            var saveData = new WorldData();
            saveData.Towns = this.towns;
            saveData.WorldLevel = this.worldLevel;
            ObserverHelper.saveToFile<WorldData>(saveData,"WorldData",id,id);
            foreach(Town town in enableTowns){
                var builder = town.compressIntoBuilder();
                ObserverHelper.saveToFile<TownBuilder>(builder, "TownData", town.getId(),id);
                MonoBehaviour.Destroy(town.gameObject);
            }
        }

        public void levelUpWorld(){
            this.worldLevel++;
        }

        private enum TownLevelDigest{
            LOW,MIDDLE,HIGH
        }

		private enum TownSizeDigest {
			SMALL, MEDIUM, BIG
		}
    }
}
