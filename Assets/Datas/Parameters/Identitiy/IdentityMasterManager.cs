using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Parameter;

namespace MasterData {
    public class IdentityMasterManager : MasterDataManagerBase {
        private static List<Identity> dataTable = new List<Identity>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/IdentityMasterData");
            constractedBehaviour(csv);
        }

        public static Identity getIdentityFromId(int id){
            foreach(Identity identity in dataTable){
                if (identity.getId() == id)
                    return identity;
            }
            throw new ArgumentException("invalid IdentityId");
        }

        public static List<Identity> getIdentitiesFromLevel(int level){
            List<Identity> identities = new List<Identity>();

            foreach(Identity identity in dataTable){
                if (identity.getLevel() >= level)
                    identities.Add(identity);
            }

            return identities;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new Identity(datas));
        }
    }
}
