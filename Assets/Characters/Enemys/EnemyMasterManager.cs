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

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
			dataTable.Add (new EnemyBuilder(datas));
		}

		#endregion
	}
}