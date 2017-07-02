using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MasterData{
	public abstract class MasterDataManagerBase  : MonoBehaviour {
		//渡されたstringの二次配列から使用するデータをstring配列で取得します
		protected string[] GetRaw (string[,] csv, int row) {
			string[] data = new string[ csv.GetLength(0) ];
			for (int i = 0; i < csv.GetLength(0); i++) 
				data[i] = csv[i, row];
			return data;
		}

		//これを継承するクラスがAwake()で実行する初期動作です
		protected void constractedBehaviour(TextAsset csvAsset){
			var datas = CSVReader.SplitCsvGrid(csvAsset.text);
			for (int i = 1; i < datas.GetLength(1) - 1 ; i++) {
				addInstance (GetRaw(datas,i));
			}
		}

		//渡されたstringデータを元にインスタンスを生成し、dataTableに登録します
		protected abstract void addInstance (string[] datas);
	}
}