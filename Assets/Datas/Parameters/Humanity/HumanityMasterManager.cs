using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterData {
    public class HumanityMasterManager : MasterDataManagerBase {
        private static readonly HumanityMasterManager INSTANCE = new HumanityMasterManager();

		private HumanityMasterManager() {
			var csv = (TextAsset)Resources.Load("MasterDatas/HumanityMasterData");
			constractedBehaviour(csv);
        }

        public static HumanityMasterManager getInstance(){
            return INSTANCE;
        }

        private List<Humanity> dataTable = new List<Humanity>();

        public Humanity getHumanityFromId(int id){
            foreach(Humanity humanity in dataTable){
                if (humanity.getId() == id)
                    return humanity;
            }
            throw new ArgumentException("invalid humanityId");
        }

        public List<Humanity> getHumanitiesFromLevel(int level){
            List<Humanity> humanities = new List<Humanity>();

            foreach(Humanity humanity in dataTable){
                if(humanity.getLevel() <= level){
                    humanities.Add(humanity);
                }
            }

            return humanities;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new Humanity(datas));
        }
    }
}