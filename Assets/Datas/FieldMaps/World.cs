using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FieldMap {
    public class World : MonoBehaviour {
        public List<GameObject> townPositions;
        private List<Town> towns;
        private GameObject townPrefab;

        private void Awake() {
            townPrefab = (GameObject)Resources.Load("Models/Town");
        }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.C))
                creatTown();

        }

        private void creatTown() {
            int townNumberMin = (townPositions.Count > 5) ? townPositions.Count - 5 : 0;
            int numberOfTown = Random.Range(townNumberMin, townPositions.Count);
            Debug.Log(numberOfTown);
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
            for (int i = 0; i < numberOfTown;i++){
                GameObject townObject = Instantiate(townPrefab, positoins[i].transform.position,new Quaternion(0, 0, 0, 0));
                Town town = townObject.GetComponent<Town>();
                town.setState(getLevel(levels[i]),getSize(sizes[i]),0);
            }
        }

        private List<GameObject> shufflePositoin(){
            List<GameObject> positions = new List<GameObject>(townPositions);
            int index = positions.Count - 1;
            while(index > 1){
                GameObject indexPosition = positions[index];
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

        private enum TownLevelDigest{
            LOW,MIDDLE,HIGH
        }

		private enum TownSizeDigest {
			SMALL, MEDIUM, BIG
		}
    }
}
