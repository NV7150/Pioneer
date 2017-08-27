using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Parameter;

namespace MasterData{
	[System.SerializableAttribute]
	public class JobMasterManager : MasterDataManagerBase {
        private static readonly JobMasterManager INSTANCE = new JobMasterManager();

		private JobMasterManager() {
			var csv = Resources.Load("MasterDatas/JobMasterData") as TextAsset;
			constractedBehaviour(csv);
        }

        public static JobMasterManager getInstance(){
            return INSTANCE;
        }


		/// <summary>
        /// 登録済みの職業のリスト
        /// </summary>
		private List<Job> dataTable = new List<Job>();

		/// <summary>
		/// IDから職業を取得します
		/// </summary>
		/// <returns>指定された職業</returns>
		/// <param name="id">取得したい職業のID</param>
		public Job getJobFromId(int id){
			foreach(Job job in dataTable){
				if (job.getId () == id)
					return job;
			}
			throw new ArgumentException ("invlit jobId");
		}

        public List<Job> getJobsFromLevel(int level){
            Debug.Log("job " + level);
            List<Job> jobs = new List<Job>();

            foreach(Job job in dataTable){
                if (job.getLevel() <= level)
                    jobs.Add(job);
            }

            return jobs;
        }

		#region implemented abstract members of MasterDataManagerBase
		protected override void addInstance(string[] datas) {
			dataTable.Add(new Job (datas));
		}
        #endregion
    }
}
