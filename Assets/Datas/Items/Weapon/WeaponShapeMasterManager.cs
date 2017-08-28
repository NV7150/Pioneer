using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Item;

namespace MasterData {
    public class WeaponShapeMasterManager : MasterDataManagerBase {
        private readonly static WeaponShapeMasterManager INSTANCE = new WeaponShapeMasterManager();
		private WeaponShapeMasterManager() {
			var csv = (TextAsset)Resources.Load("MasterDatas/WeaponShapeMasterData");
			constractedBehaviour(csv);
        }

        public static WeaponShapeMasterManager getInstance(){
            return INSTANCE;
        }

        private List<WeaponShape> dataTable = new List<WeaponShape>();

        public WeaponShape getShapeFromId(int id){
            foreach(WeaponShape shape in dataTable){
                if (shape.getId() == id)
                    return shape;
            }
            throw new ArgumentException("invalid shapeId " + id);
        }

        public int getNumberOfShapes(){
            return dataTable.Count;
        }

        protected override void addInstance(string[] datas) {
            dataTable.Add(new WeaponShape(datas));
        }
    }
}
