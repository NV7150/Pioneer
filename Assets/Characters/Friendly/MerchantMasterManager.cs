using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using FieldMap;

namespace MasterData {
    public class MerchantMasterManager : MasterDataManagerBase {
        private static List<MerchantBuiler> dataTable = new List<MerchantBuiler>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/MerchantMasterData");
            constractedBehaviour(csv);
        }

        public static Merchant getMerchantFromId(int id,Town livingTown){
            foreach(MerchantBuiler builder in dataTable){
                if (builder.getId() == id)
                    return builder.build(livingTown);
            }
            throw new ArgumentException("invalid id");
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new MerchantBuiler(datas));
        }
    }
}
