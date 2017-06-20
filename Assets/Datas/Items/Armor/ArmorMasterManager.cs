using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Item;

namespace MasterData {
	[System.SerializableAttribute]
	public class ArmorMasterManager :MasterDataManagerBase {
		[SerializeField]
		List<ArmorBuilder> dataSet = new List<ArmorBuilder>();

		void Awake(){
			var armorCSVData = Resources.Load ("MasterDatas/ArmorMasterData") as TextAsset;
			awakeBehaviour (armorCSVData);
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addToDataList (string[,] datas, int index) {
			ArmorBuilder builder = new ArmorBuilder (GetRaw(datas,index));
			dataSet.Add (builder);
		}

		#endregion

		public Armor getArmorFromId(int id){
			foreach (ArmorBuilder builder in dataSet)
				if (builder.getId () == id)
					return builder.build ();
			throw new ArgumentException ("invlit armorID");
		}
	}
}

