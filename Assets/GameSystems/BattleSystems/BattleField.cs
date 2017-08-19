using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

namespace BattleSystem{
	public class BattleField{
		/// <summary> バトルフィールドの起点となる位置 </summary>
		private readonly Vector3 STARTER_POSITON;
		/// <summary> FieldPosition間の実距離 </summary>
		private static readonly int distanceOfArea = 10;
        private static readonly int widthOfArea = 15;

        private Dictionary<FieldPosition, List<List<bool>>> canSetCharacterPos = new Dictionary<FieldPosition, List<List<bool>>>();
        private Dictionary<IBattleable, KeyValuePair<FieldPosition, KeyValuePair<int, int>>> characterPosition = new Dictionary<IBattleable, KeyValuePair<FieldPosition, KeyValuePair<int, int>>>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">起点</param>
		public BattleField(Vector3 position){
			STARTER_POSITON = position;

            int vertical = distanceOfArea;
            int holizontal = widthOfArea;

            var keys = Enum.GetValues(typeof(FieldPosition));
            foreach (FieldPosition key in keys) {
                List<List<bool>> positionInfo = new List<List<bool>>();
                for (int i = 0; i < vertical; i++) {
                    List<bool> line = new List<bool>();
                    for (int j = 0; j < holizontal; j++) {
                        line.Add(true);
                    }
                    positionInfo.Add(line);
                }
                canSetCharacterPos.Add(key,positionInfo);
            }
		}

		/// <summary>
		/// 与えられたポジションの座標を取得します
		/// </summary>
		/// <returns>ポジションの座標</returns>
		/// <param name="position">Going.</param>
        public Vector3 getObjectPosition(FieldPosition position,IBattleable bal) {
            
            //空いている座標を検索し、その座標をrandomSetに格納
            List<KeyValuePair<int, int>> randomSet = new List<KeyValuePair<int, int>>();

			int zPos = 0;
            foreach(List<bool> line in canSetCharacterPos[position]){
				int xPos = 0;
                foreach(bool pos in line){
                    if(pos){
                        KeyValuePair<int,int> positon = new KeyValuePair<int, int>(xPos,zPos);
                        randomSet.Add(positon);
                    }
                    xPos++;
                }
                zPos++;
            }

            //移動するところを決定（randomSetには空いている座標が入っているのでどのインデックスのものを取得するかランダム）
            int random = UnityEngine.Random.Range(0, randomSet.Count);
            int x = randomSet[random].Key;
            int z = randomSet[random].Value;

            //移動するところの周囲１マスを埋める
            for (int i = -1; i < 2;i++){
                int targetZ = z + i;
				if (targetZ < 0) {
					targetZ = 0;
				} else if (targetZ >= canSetCharacterPos.Count) {
					targetZ = canSetCharacterPos.Count - 1;
				}

                for (int j = -1; j < 2; j++){
                    int targetX = x + j;
                    if(targetX < 0){
                        targetX = 0;
                    }else if(targetX >= canSetCharacterPos.Count){
                        targetX = canSetCharacterPos.Count - 1;
                    }

                    canSetCharacterPos[position][targetX][targetZ] = false;
                }
            }
            KeyValuePair<FieldPosition, KeyValuePair<int, int>> characterRawPos = new KeyValuePair<FieldPosition, KeyValuePair<int, int>>(position, randomSet[random]);
            characterPosition.Add(bal,characterRawPos);

            int realX = x - widthOfArea / 2;
            int realZ = (z - distanceOfArea / 2) + (distanceOfArea * ((int)position - 3));
			return STARTER_POSITON + new Vector3 (realX,0,realZ);
		}

        public void deleteCharacterPos(IBattleable bal){
            KeyValuePair<FieldPosition, KeyValuePair<int, int>> postionPair = characterPosition[bal];
            FieldPosition fieldPos = postionPair.Key;
            int x = postionPair.Value.Key;
            int z = postionPair.Value.Value;
            //居たところの周囲１マスを開ける
			for (int i = -1; i < 2; i++) {
				int targetZ = z + i;
				if (targetZ < 0) {
					targetZ = 0;
				} else if (targetZ >= canSetCharacterPos.Count) {
					targetZ = canSetCharacterPos.Count - 1;
				}

				for (int j = -1; j < 2; j++) {
					int targetX = x + j;
					if (targetX < 0) {
						targetX = 0;
					} else if (targetX >= canSetCharacterPos.Count) {
						targetX = canSetCharacterPos.Count - 1;
					}

                    canSetCharacterPos[fieldPos][targetX][targetZ] = true;
				}
			}
            characterPosition.Remove(bal);
        }
	}
}
