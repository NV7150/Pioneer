using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Item;

namespace MasterData {
	[System.SerializableAttribute]
	public class ArmorMasterManager :MasterDataManagerBase {
		private static List<ArmorBuilder> dataTable = new List<ArmorBuilder>();

		void Awake(){
			var armorCSVData = Resources.Load ("MasterDatas/ArmorMasterData") as TextAsset;
			constractedBehaviour (armorCSVData);
		}

		public static Armor getArmorFromId(int id){
			foreach (ArmorBuilder builder in dataTable)
				if (builder.getId () == id)
					return builder.build ();
			throw new ArgumentException ("invlit armorID");
		}
		#region implemented abstract members of MasterDataManagerBase

		protected override void addInstance (string[] datas) {
			dataTable.Add(new ArmorBuilder (datas));
		}

		#endregion
	}
}

