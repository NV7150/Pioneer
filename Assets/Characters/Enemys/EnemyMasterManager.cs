using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MasterData;
using System;

using Character;

namespace MasterData{
	[System.SerializableAttribute]
	public class EnemyMasterManager : MasterDataManagerBase{
		private static List<EnemyBuilder> dataTable = new List<EnemyBuilder>();

		private void Awake(){
			var enemyCSVText = Resources.Load("MasterDatas/EnemyMasterData") as TextAsset;
			constractedBehaviour (enemyCSVText);
		}

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