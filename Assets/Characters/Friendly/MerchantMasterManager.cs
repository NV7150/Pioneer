using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;
using FieldMap;

namespace MasterData {
    public class MerchantMasterManager : MasterDataManagerBase {
        private static readonly MerchantMasterManager INSTANCE = new MerchantMasterManager();

        public static MerchantMasterManager getInstance(){
            return INSTANCE;
        }

        private MerchantMasterManager(){
			var csv = (TextAsset)Resources.Load("MasterDatas/MerchantMasterData");
			constractedBehaviour(csv);
        }

        private List<MerchantBuiler> dataTable = new List<MerchantBuiler>();

        public Merchant getMerchantFromId(int id,Town livingTown){
            foreach(MerchantBuiler builder in dataTable){
                if (builder.getId() == id)
                    return builder.build(livingTown);
            }
            throw new ArgumentException("invalid id");
        }

        public MerchantBuiler getMerchantBuilderFromId(int id){
			foreach (MerchantBuiler builder in dataTable) {
                if (builder.getId() == id)
                    return builder;
			}
			throw new ArgumentException("invalid id");
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new MerchantBuiler(datas));
        }
    }
}
