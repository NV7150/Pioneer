using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSV;
using System;

using character;

namespace masterData{
	[System.SerializableAttribute]
	public class EnemyMasterManager : MonoBehaviour {
		[SerializeField]
		private List<Enemy> dataTable = new List<Enemy> ();

		private void Awake(){
			var enemyCSVText = Resources.Load("MasterDatas/EnemyMasterdata") as TextAsset;
			var datas = CSV.CSVReader.SplitCsvGrid(enemyCSVText.text);
			for (int i = 1; i < datas.GetLength(1) - 1 ; i++) {
				Enemy data = new Enemy(GetRaw(datas,i));
				dataTable.Add (data);
			}
		}

		private string[] GetRaw (string[,] csv, int row) {
			string[] data = new string[ csv.GetLength(0) ];
			for (int i = 0; i < csv.GetLength(0); i++) 
				data[i] = csv[i, row];
			return data;
		}

		public Enemy getEnemyFromId(int id){
			foreach(Enemy enemy in dataTable){
				if (enemy.getId () == id)
					return enemy.Clone();
			}
			throw new ArgumentException ("invalit enemyId " + id);
		}
	}
}