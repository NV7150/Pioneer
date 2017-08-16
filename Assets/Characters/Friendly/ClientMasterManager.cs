using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using System;
using FieldMap;

namespace MasterData {
	public class ClientMasterManager : MasterDataManagerBase {
		private static List<ClientBuilder> dataTable = new List<ClientBuilder>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/ClientMasterData");
            constractedBehaviour(csv);
        }

        public static Client getClientFromId(int id,Town town){
            foreach(ClientBuilder builder in dataTable){
                if (builder.getId() == id)
                    return builder.build(town);
            }
            throw new ArgumentException("invalid clientId");
        }

        public static ClientBuilder getClientBuilderFromId(int id) {
			foreach (ClientBuilder builder in dataTable) {
				if (builder.getId() == id)
					return builder;
			}
			throw new ArgumentException("invalid clientId");
		}

        protected override void addInstance(string[] datas) {
            dataTable.Add(new ClientBuilder(datas));
		}
    }
}

