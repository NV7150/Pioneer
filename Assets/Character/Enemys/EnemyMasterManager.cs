using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSV;
using System;

using character;

namespace masterData{
	[System.SerializableAttribute]
	public class EnemyMasterManager : MasterDataManagerBase{
		[SerializeField]
		private List<EnemyBuilder> dataTable = new List<EnemyBuilder> ();

		private void Awake(){
			var enemyCSVText = Resources.Load("Masterdatas/EnemyMasterdata") as TextAsset;
			awakeBehaviour (enemyCSVText);
		}

		public Enemy getEnemyFromId(int id){
			foreach(EnemyBuilder builder in dataTable){
				if (builder.getId () == id)
					return builder.build ();
			}
			throw new ArgumentException ("invalit enemyId " + id);
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addToDataList (string[,] datas, int index) {
			EnemyBuilder builder = new EnemyBuilder(GetRaw (datas, index));
			dataTable.Add (builder);
		}

		#endregion
	}
}