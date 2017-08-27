using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterData {
    public class ArmorShapeMasterManager : MasterDataManagerBase {
        private static readonly ArmorShapeMasterManager INSTANCE = new ArmorShapeMasterManager();

        public static ArmorShapeMasterManager getInstance(){
            return INSTANCE;
        }

        private ArmorShapeMasterManager(){
			var csv = (TextAsset)Resources.Load("MasterDatas/ArmorShapeMasterData");
			constractedBehaviour(csv);
        }

        private List<ArmorShape> dataTable = new List<ArmorShape>();

        public ArmorShape getShapeFromId(int id){
            foreach(ArmorShape shape in dataTable){
                if (shape.getId() == id)
                    return shape;
            }
            throw new ArgumentException("invalid ArmorShapeId");
        }

        public int getNumberOfShapes(){
            return dataTable.Count;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new ArmorShape(datas));
        }
    }
}
