using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Parameter;

namespace MasterData{
	[System.SerializableAttribute]
	public class JobMasterManager : MasterDataManagerBase {
		private static List<Job> dataTable = new List<Job>();

		// Use this for initialization
		void Awake() {
			var csv = Resources.Load ("MasterDatas/JobMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		public static Job getJobFromId(int id){
			foreach(Job job in dataTable){
				if (job.getId () == id)
					return job;
			}
			throw new ArgumentException ("invlit jobId");
		}
			
		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance (string[] datas) {
			dataTable.Add(new Job (datas));
		}
		#endregion
	}
}
