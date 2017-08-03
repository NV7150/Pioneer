using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class WeponShapeMasterManager : MasterDataManagerBase {
        private static List<WeponShape> dataTable = new List<WeponShape>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/WeponShapeMasterData");
            constractedBehaviour(csv);
        }

        public static WeponShape getShapeFromId(int id){
            foreach(WeponShape shape in dataTable){
                if (shape.getId() == id)
                    return shape;
            }
            throw new ArgumentException("invalid shapeId");
        }

        public static int getNumberOfShapes(){
            return dataTable.Count;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new WeponShape(datas));
        }
    }
}
