using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MasterData;
using System;

using Character;

namespace MasterData{
	[System.SerializableAttribute]
	public class EnemyMasterManager : MasterDataManagerBase<EnemyBuilder>{

		private void Start(){
			var enemyCSVText = Resources.Load("MasterDatas/EnemyMasterData") as TextAsset;
			constractedBehaviour (enemyCSVText);
		}

		public Enemy getEnemyFromId(int id){
			foreach(EnemyBuilder builder in dataTable){
				if (builder.getId () == id) {
					return builder.build ();
				}
			}
			throw new ArgumentException ("invalit enemyId " + id);
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override EnemyBuilder getInstance (int index, string[,] args) {
			return new EnemyBuilder (GetRaw(args,index));
		}

		#endregion
	}
}