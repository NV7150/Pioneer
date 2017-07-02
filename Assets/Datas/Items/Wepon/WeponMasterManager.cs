using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Item;

namespace MasterData {
	[System.SerializableAttribute]
	public class WeponMasterManager :MasterDataManagerBase {
		//登録済みのデータのリストです
		private static List<WeponBuilder> dataTable = new List<WeponBuilder>();
		
		void Awake(){
			var weponCSVText = Resources.Load("MasterDatas/WeponMasterData") as TextAsset;
			constractedBehaviour (weponCSVText);
		}

		//IDからWeponを取得します
		public static Wepon getWeponFromId(int id){
			foreach (WeponBuilder builder in dataTable)
				if (builder.getId() == id)
					return builder.build ();
			throw new ArgumentException ("invlit weponId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
			dataTable.Add(new WeponBuilder (datas));
		}

		#endregion
	}
}