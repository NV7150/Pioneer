using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Parameter;

namespace MasterData{
	[System.SerializableAttribute]
	public class JobMasterManager : MasterDataManagerBase<Job> {
		// Use this for initialization
		void Start () {
			var csv = Resources.Load ("MasterDatas/JobMasterData") as TextAsset;
			constractedBehaviour (csv);
		}

		public Job getJobFromId(int id){
			foreach(Job job in dataTable){
				if (job.getId () == id)
					return job;
			}
			throw new ArgumentException ("invlit jobId");
		}
			
		#region implemented abstract members of MasterDataManagerBase
		protected override Job getInstance (int index, string[,] args) {
			return new Job (GetRaw(args,index));
		}
		#endregion
	}
}
