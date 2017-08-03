using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterData {
    public class ArmorShapeManager : MasterDataManagerBase {
        private static List<ArmorShape> dataTable = new List<ArmorShape>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/ArmorShapeMasterData");
            constractedBehaviour(csv);
        }

        public static ArmorShape getShapeFromId(int id){
            foreach(ArmorShape shape in dataTable){
                if (shape.getId() == id)
                    return shape;
            }
            throw new ArgumentException("invalid ArmorShapeId");
        }

        public static int getNumberOfShapes(){
            return dataTable.Count;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new ArmorShape(datas));
        }
    }
}
