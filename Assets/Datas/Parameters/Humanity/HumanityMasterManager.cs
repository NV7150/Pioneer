using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterData {
    public class HumanityMasterManager : MasterDataManagerBase {
        private static List<Humanity> dataTable = new List<Humanity>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/HumanityMasterData");
            constractedBehaviour(csv);
        }

        public static Humanity getHumanityFromId(int id){
            foreach(Humanity humanity in dataTable){
                if (humanity.getId() == id)
                    return humanity;
            }
            throw new ArgumentException("invalid humanityId");
        }

        public static List<Humanity> getHumanitiesFromLevel(int level){
            List<Humanity> humanities = new List<Humanity>();

            foreach(Humanity humanity in dataTable){
                if(humanity.getLevel() >= level){
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