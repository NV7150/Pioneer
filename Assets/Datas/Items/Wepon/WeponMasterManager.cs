using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Item;

namespace MasterData {
	[System.SerializableAttribute]
	public class WeponMasterManager :MasterDataManagerBase<WeponBuilder> {
		
		void Awake(){
			var weponCSVText = Resources.Load("MasterDatas/WeponMasterData") as TextAsset;
			constractedBehaviour (weponCSVText);
		}

		public Wepon getWeponFromId(int id){
			foreach (WeponBuilder builder in dataTable)
				if (builder.getId() == id)
					return builder.build ();
			throw new ArgumentException ("invlit weponId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override WeponBuilder getInstance (int index, string[,] args) {
			return new WeponBuilder (GetRaw(args,index));
		}

		#endregion
	}
}