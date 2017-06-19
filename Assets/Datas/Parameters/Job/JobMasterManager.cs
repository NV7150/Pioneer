using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Parameter;

namespace MasterData{
	public class JobMasterManager : MasterDataManagerBase {
		List<Job> dataTable = new List<Job>();

		// Use this for initialization
		void Awake () {
			var csv = Resources.Load ("Masterdatas/JobMasterdata") as TextAsset;
			awakeBehaviour (csv);
		}

		public Job getJobFromId(int id){
			foreach(Job job in dataTable){
				if (job.getId () == id)
					return job;
			}
			throw new ArgumentException ("invlit jobId");
		}

		#region implemented abstract members of MasterDataManagerBase

		protected override void addToDataList (string[,] datas, int index) {
			dataTable.Add (new Job(GetRaw(datas,index)));
		}

		#endregion
	}
}
