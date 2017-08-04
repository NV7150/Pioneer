using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class WeaponShapeMasterManager : MasterDataManagerBase {
        private static List<WeaponShape> dataTable = new List<WeaponShape>();

        private void Awake() {
            var csv = (TextAsset)Resources.Load("MasterDatas/WeaponShapeMasterData");
            constractedBehaviour(csv);
        }

        public static WeaponShape getShapeFromId(int id){
            foreach(WeaponShape shape in dataTable){
                if (shape.getId() == id)
                    return shape;
            }
            throw new ArgumentException("invalid shapeId");
        }

        public static int getNumberOfShapes(){
            return dataTable.Count;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new WeaponShape(datas));
        }
    }
}
