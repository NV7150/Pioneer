using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Item;

namespace MasterData {
	[System.SerializableAttribute]
	public class ArmorMasterManager :MasterDataManagerBase {
		/// <summary> 生成したArmorBuilderのリスト </summary>
		private static List<ArmorBuilder> dataTable = new List<ArmorBuilder>();

		void Awake(){
			var armorCSVData = Resources.Load ("MasterDatas/ArmorMasterData") as TextAsset;
			constractedBehaviour (armorCSVData);
		}

		/// <summary>
		/// IDから防具を取得します
		/// </summary>
		/// <returns>指定した防具</returns>
		/// <param name="id">取得したい防具のID</param>
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

