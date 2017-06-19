using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterData{
	public abstract class MasterDataManagerBase : MonoBehaviour {
		protected string[] GetRaw (string[,] csv, int row) {
			string[] data = new string[ csv.GetLength(0) ];
			for (int i = 0; i < csv.GetLength(0); i++) 
				data[i] = csv[i, row];
			return data;
		}

		protected void awakeBehaviour(TextAsset csvAsset){
			var datas = CSVReader.SplitCsvGrid(csvAsset.text);
			for (int i = 1; i < datas.GetLength(1) - 1 ; i++) {
				addToDataList (datas,i);
			}
		}

		protected abstract void addToDataList (string[,] datas,int index);
	}
}