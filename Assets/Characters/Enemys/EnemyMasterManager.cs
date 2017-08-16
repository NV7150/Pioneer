using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MasterData;
using System;

using Character;

namespace MasterData{
	[System.SerializableAttribute]
	public class EnemyMasterManager : MasterDataManagerBase{
		/// <summary> 登録済みEnemySのリスト </summary>
		private static List<EnemyBuilder> dataTable = new List<EnemyBuilder>();
        private static Dictionary<int, EnemyProgress> progressTable = new Dictionary<int, EnemyProgress>();

		private void Awake(){
			var enemyCSVText = Resources.Load("MasterDatas/EnemyMasterData") as TextAsset;
			constractedBehaviour (enemyCSVText);
		}

		/// <summary>
		/// idからEnemyを生成し、返します
		/// </summary>
		/// <returns>結果のEnemy</returns>
		/// <param name="id">取得したいEnemyのID</param>
		public static Enemy getEnemyFromId(int id){
			foreach(EnemyBuilder builder in dataTable){
				if (builder.getId () == id) {
					return builder.build ();
				}
			}
			throw new ArgumentException ("invalit enemyId " + id);
		}

		public static EnemyBuilder getEnemyBuilderFromId(int id) {
			foreach (EnemyBuilder builder in dataTable) {
                if (builder.getId () == id) {
                    return builder;
                }
            }
            throw new ArgumentException ("invalit enemyId " + id);
        }

        public static List<int> getEnemyIdsFromLevel(int level){
            var ids = new List<int>();
            foreach(EnemyBuilder builder in dataTable){
                if (builder.getLevel() == level)
                    ids.Add(builder.getId());
            }
            return ids;
        }

        public static string getEnemyNameFromId(int id){
            foreach(EnemyBuilder builder in dataTable){
                if(builder.getId() == id){
                    return builder.getName();
                }
			}
			throw new ArgumentException("invalit enemyId " + id);
        }

        public static EnemyProgress getProgressFromId(int id){
            return progressTable[id];
        }

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
            var builder = new EnemyBuilder(datas);
            dataTable.Add (builder);
            int id = int.Parse(datas[0]);
            if (ES2.Exists(getLoadPass(id, "EnemyProgress.txt"))) {
                var progress = loadSaveData<EnemyProgress>(id, "EnemyProgress.txt");
                builder.setProgress(progress);
                progressTable.Add(id, progress);
            }else{
                progressTable.Add(id,new EnemyProgress());
            }
		}

        #endregion
    }
}