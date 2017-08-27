using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

namespace MasterData {
    public class CitizenMasterManager : MasterDataManagerBase {
        private static readonly CitizenMasterManager INSTANCE = new CitizenMasterManager();

		private CitizenMasterManager() {
			TextAsset csv = (TextAsset)Resources.Load("MasterDatas/CitizenMasterData");
			constractedBehaviour(csv);
        }

        public static CitizenMasterManager getInstance(){
            return INSTANCE;
        }

        /// <summary> 生成したCitizenBuilderのリスト </summary>
        public List<CitizenBuilder> dataTable = new List<CitizenBuilder>();

        protected override void addInstance(string[] datas) {
            dataTable.Add(new CitizenBuilder(datas));
		}

        /// <summary>
        /// 指定されたidのCitizenを取得します
        /// </summary>
        /// <returns>指定されたidのCitizen</returns>
        /// <param name="id">取得したいCitizenのid</param>
        public Citizen getCitizenFromId(int id){
            foreach(CitizenBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder.build();
            }
            throw new ArgumentException("invalid citizenId");
        }

		public CitizenBuilder getCitizenBuilderFromId(int id) {
			foreach (CitizenBuilder builder in dataTable) {
				if (builder.getId() == id)
					return builder;
			}
			throw new ArgumentException("invalid citizenId");
        }
    }
}
