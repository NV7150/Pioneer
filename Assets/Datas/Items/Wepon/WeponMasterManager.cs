using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using item;

namespace masterdata {
	[System.SerializableAttribute]
	public class WeponMasterManager :MasterDataManagerBase {
		[SerializeField]
		List<WeponBuilder> dataSet = new List<WeponBuilder>();

		void Awake(){
			var weponCSVText = Resources.Load("Masterdatas/WeponMasterdata") as TextAsset;
			awakeBehaviour (weponCSVText);
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addToDataList (string[,] datas, int index) {
			WeponBuilder builder = new WeponBuilder (GetRaw(datas,index));
			dataSet.Add (builder);
		}

		#endregion

		public Wepon getWeponFromId(int id){
			foreach (WeponBuilder builder in dataSet)
				if (builder.getId() == id)
					return builder.build ();
			throw new ArgumentException ("invlit weponId");
		}
	}
}