using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MasterData;
using System;

using Character;

namespace MasterData{
    [System.SerializableAttribute]
    public class EnemyMasterManager : MasterDataManagerBase {
        private static readonly EnemyMasterManager INSTANCE = new EnemyMasterManager();

        public static EnemyMasterManager getInstance() {
            return INSTANCE;
        }

		private EnemyMasterManager() {
			var enemyCSVText = Resources.Load("MasterDatas/EnemyMasterData") as TextAsset;
            constractedBehaviour(enemyCSVText);
        }

		/// <summary> 登録済みEnemySのリスト </summary>
		private List<EnemyBuilder> dataTable = new List<EnemyBuilder>();
        private Dictionary<int, EnemyProgress> progressTable = new Dictionary<int, EnemyProgress>();

		/// <summary>
		/// idからEnemyを生成し、返します
		/// </summary>
		/// <returns>結果のEnemy</returns>
		/// <param name="id">取得したいEnemyのID</param>
		public Enemy getEnemyFromId(int id){
			foreach(EnemyBuilder builder in dataTable){
				if (builder.getId () == id) {
					return builder.build ();
				}
			}
			throw new ArgumentException ("invalit enemyId " + id);
		}

		public EnemyBuilder getEnemyBuilderFromId(int id) {
			foreach (EnemyBuilder builder in dataTable) {
                if (builder.getId () == id) {
                    return builder;
                }
            }
            throw new ArgumentException ("invalit enemyId " + id);
        }

        /// <summary>
        /// 指定されたレベル以下エネミーの 、IDをキー、レベルを値とするディクショナリを返します
        /// </summary>
        /// <returns>The enemy identifiers from level.</returns>
        /// <param name="level">Level.</param>
        public Dictionary<int,int> getEnemyIdsFromLevel(int level){
            var ids = new Dictionary<int,int>();
            foreach(EnemyBuilder builder in dataTable){
                if (builder.getLevel() <= level)
                    ids.Add(builder.getId(),builder.getLevel());
            }
            return ids;
        }

        public string getEnemyNameFromId(int id){
            foreach(EnemyBuilder builder in dataTable){
                if(builder.getId() == id){
                    return builder.getName();
                }
			}
			throw new ArgumentException("invalit enemyId " + id);
        }

        public EnemyProgress getProgressFromId(int id){
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