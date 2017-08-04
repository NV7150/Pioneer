using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using System;

namespace MasterData {
	public class ClientMasterManager : MasterDataManagerBase {
		private static List<ClientBuilder> dataTable = new List<ClientBuilder>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/ClientMasterData");
            constractedBehaviour(csv);
        }

        public static Client getClientFromId(int id){
            foreach(ClientBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder.build();
            }
            throw new ArgumentException("invalid clientId");
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new ClientBuilder(datas));
		}
	}
}

