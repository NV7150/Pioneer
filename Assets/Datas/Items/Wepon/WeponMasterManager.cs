using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Item;

namespace MasterData {
	[System.SerializableAttribute]
	public class WeponMasterManager :MasterDataManagerBase {
		/// <summary> 登録したWeponBuilderのリスト </summary>
		private static List<WeponBuilder> dataTable = new List<WeponBuilder>();
		
		void Awake(){
			var weponCSVText = Resources.Load("MasterDatas/WeponMasterData") as TextAsset;
			constractedBehaviour (weponCSVText);
		}

		/// <summary>
        /// IDからWeponを取得します
        /// </summary>
        /// <returns>指定されたWepon</returns>
        /// <param name="id">取得したい武器のID</param>
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